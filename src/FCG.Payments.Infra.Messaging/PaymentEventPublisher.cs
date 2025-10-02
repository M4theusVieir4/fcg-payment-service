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
}
