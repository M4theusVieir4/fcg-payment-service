using FCG.Payments.Domain;

namespace FCG.Payments.Application.Contracts;
public record CreatePaymentOutput(
    Guid Id,
    Guid OrderId,
    Guid UserId,
    decimal Amount,
    string Currency,
    string Status,
    string PaymentMethod,
    string Provider,
    DateTime CreatedAt,
    DateTime UpdatedAt
)
{
    public CreatePaymentOutput(Payment payment)
        : this(
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
        )
    {}
}
