namespace FCG.Payments.Domain;
public interface IPaymentEventPublisher
{
    Task PublishPaymentCreatedAsync(Payment payment, CancellationToken ct = default);
}
