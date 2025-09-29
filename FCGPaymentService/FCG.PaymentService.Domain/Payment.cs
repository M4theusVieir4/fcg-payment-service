using FCG.PaymentService.Domain._Common;

namespace FCG.PaymentService.Domain;
public class Payment(
        Guid paymentId,
        Guid orderId,
        Guid userId,
        decimal amount,
        string currency,
        string status,
        string paymentMeth,
        string provider,    
        DateTime createdAt,
        DateTime updatedAt
) : EntityBase(paymentId)
{
    public Guid PaymentId { get; private set; } = paymentId;
    public Guid OrderId { get; private set; } = orderId;
    public Guid UserId { get; private set; } = orderId;
    public decimal Amount { get; private set; } = amount;
    public string Currency { get; private set; } = currency;
    public string Status { get; private set; } = status;
    public string PaymentMethod { get; private set; } = paymentMeth;
    public string Provider { get; private set; } = provider;
    public DateTime CreatedAt { get; private set; } = createdAt;
    public DateTime UpdatedAt { get; private set; } = updatedAt;
}
