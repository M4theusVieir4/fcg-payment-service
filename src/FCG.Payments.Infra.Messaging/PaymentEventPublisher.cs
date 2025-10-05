using Amazon.SQS;
using Amazon.SQS.Model;
using FCG.Payments.Domain;
using Newtonsoft.Json;

namespace FCG.Payments.Infra.Messaging;
public class PaymentEventPublisher : IPaymentEventPublisher
{
    private readonly IAmazonSQS _sqs;
    private readonly string _queueUrl;

    public PaymentEventPublisher(IAmazonSQS sqs, string queueUrl)
    {
        _sqs = sqs;
        _queueUrl = queueUrl;
    }

    public async Task PublishPaymentCreatedAsync(Payment payment, CancellationToken ct = default)
    {
        var message = JsonConvert.SerializeObject(new
        {
            payment.Id,
            payment.OrderId,
            payment.UserId,
            payment.Amount,
            payment.Currency,
            payment.Status,
            payment.PaymentMethod,
            payment.Provider,
            payment.CreatedAt,
            payment.UpdatedAt
        });

        var request = new SendMessageRequest
        {
            QueueUrl = _queueUrl,
            MessageBody = message,
            MessageGroupId = "payments-group",
            MessageDeduplicationId = Guid.NewGuid().ToString()
        };

        await _sqs.SendMessageAsync(request, ct);
    }

    public async Task<List<Payment>> ListMessagesAsync(int maxMessages = 10, CancellationToken ct = default)
    {
        var request = new ReceiveMessageRequest
        {
            QueueUrl = _queueUrl,
            MaxNumberOfMessages = maxMessages,
            WaitTimeSeconds = 0, 
            VisibilityTimeout = 0 
        };

        var response = await _sqs.ReceiveMessageAsync(request, ct);

        var payments = new List<Payment>();

        foreach (var message in response.Messages)
        {            
            var payment = JsonConvert.DeserializeObject<Payment>(message.Body);
            if (payment != null)
                payments.Add(payment);        
            
        }

        return payments;
    }
}
