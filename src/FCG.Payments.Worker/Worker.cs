using Amazon.SQS;
using Amazon.SQS.Model;
using FCG.Payments.Worker.Dto;
using Polly;
using Polly.Retry;
using System.Text.Json;

namespace FCG.Payments.Worker;
public class Worker : BackgroundService
{
    private readonly IAmazonSQS _sqs;
    private readonly string _queueUrl;
    private readonly ILogger<Worker> _logger;
    private readonly AsyncRetryPolicy _retryPolicy;

    public Worker(IAmazonSQS sqs, IConfiguration configuration, ILogger<Worker> logger)
    {
        _sqs = sqs;
        _queueUrl = configuration["AWS:SQS:PaymentsQueueUrl"]
                    ?? throw new ArgumentNullException("AWS:SQS:PaymentsQueueUrl");
        _logger = logger;

        _retryPolicy = Policy
            .Handle<Exception>()
            .WaitAndRetryAsync(
                retryCount: 3,
                sleepDurationProvider: attempt => TimeSpan.FromSeconds(5),
                onRetry: (exception, timeSpan, attempt, context) =>
                {
                   _logger.LogWarning(exception, "Tentativa {Attempt} falhou, tentando novamente em {Delay}s", attempt, timeSpan.TotalSeconds);
                }
            );
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Worker iniciado em: {time}", DateTimeOffset.Now);

        while (!stoppingToken.IsCancellationRequested)
        {
            var request = new ReceiveMessageRequest
            {
                QueueUrl = _queueUrl,
                MaxNumberOfMessages = 10,
                WaitTimeSeconds = 10
            };

            var response = await _sqs.ReceiveMessageAsync(request, stoppingToken);

            if (response.Messages is null || !response.Messages.Any())
            {
                await Task.Delay(1000, stoppingToken);
                continue;
            }

            foreach (var message in response.Messages)
            {
                _ = ProcessMessageWithVisibilityRenewalAsync(message, stoppingToken);
            }

            await Task.Delay(1000, stoppingToken);
        }
    }

    private async Task ProcessMessageWithVisibilityRenewalAsync(Message message, CancellationToken stoppingToken)
    {
        using var cts = CancellationTokenSource.CreateLinkedTokenSource(stoppingToken);

      
        var renewalTask = RenewVisibilityTimeoutAsync(message, cts.Token);

        try
        {
            var payment = JsonSerializer.Deserialize<PaymentMessage>(message.Body);
            if (payment != null)
            {
                await ProcessPaymentAsync(payment);
            }

            
            await _sqs.DeleteMessageAsync(_queueUrl, message.ReceiptHandle, stoppingToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao processar pagamento da fila");
        }
        finally
        {
            cts.Cancel(); 
            await renewalTask;
        }
    }

    private async Task RenewVisibilityTimeoutAsync(Message message, CancellationToken token)
    {
        try
        {
            while (!token.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(25), token);

                await _sqs.ChangeMessageVisibilityAsync(new ChangeMessageVisibilityRequest
                {
                    QueueUrl = _queueUrl,
                    ReceiptHandle = message.ReceiptHandle,
                    VisibilityTimeout = 30 
                }, token);
            }
        }
        catch (TaskCanceledException)
        {
          
        }
    }

    private async Task ProcessPaymentAsync(PaymentMessage payment)
    {
        _logger.LogInformation("Processando pagamento {PaymentId} para Order {OrderId}",
            payment.Id, payment.OrderId);

        await _retryPolicy.ExecuteAsync(async () =>
        {
            _logger.LogInformation("Iniciando processamento do pagamento {PaymentId} para Order {OrderId}", payment.Id, payment.OrderId);

            await Task.Delay(2000); 

            _logger.LogInformation("Pagamento {PaymentId} processado com sucesso", payment.Id);
        });
    }
}
