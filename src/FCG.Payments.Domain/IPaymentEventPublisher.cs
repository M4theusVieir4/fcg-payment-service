namespace FCG.Payments.Domain;
public interface IPaymentEventPublisher
{
    Task PublishPaymentCreatedAsync(Payment payment, CancellationToken ct = default);

    Task<List<Payment>> ListMessagesAsync(int maxMessages = 10, CancellationToken ct = default);
}
