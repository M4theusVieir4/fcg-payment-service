namespace FCG.Payments.Domain;
public interface IPaymentRepository
{
    Task<Payment?> GetByIdAsync(Guid id, CancellationToken ct);

    Task<bool> ExistByOrderIdAsync(
        Guid orderId,
        Guid? ignoreId = null,
        CancellationToken ct = default
    );

    Task IndexAsync(Payment payment, CancellationToken ct);
}
