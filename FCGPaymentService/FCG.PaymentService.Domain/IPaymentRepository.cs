namespace FCG.PaymentService.Domain;
public interface IPaymentRepository
{
    Task<Payment?> GetByIdAsync(Guid id, CancellationToken ct);
}
