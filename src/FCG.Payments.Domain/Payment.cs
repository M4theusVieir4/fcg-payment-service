using FCG.Payments.Domain._Common;

namespace FCG.Payments.Domain;
public class Payment(
        Guid? id,
        Guid orderId,
        Guid userId,
        decimal amount,
        string currency,
        string status,
        string paymentMeth,
        string provider,    
        DateTime createdAt,
        DateTime updatedAt
) : EntityBase(id)
{    
    public Guid OrderId { get; private set; } = orderId;
    public Guid UserId { get; private set; } = userId;
    public decimal Amount { get; private set; } = amount;
    public string Currency { get; private set; } = currency;
    public string Status { get; private set; } = status;
    public string PaymentMethod { get; private set; } = paymentMeth;
    public string Provider { get; private set; } = provider;
    public DateTime CreatedAt { get; private set; } = createdAt;
    public DateTime UpdatedAt { get; private set; } = updatedAt;
}
