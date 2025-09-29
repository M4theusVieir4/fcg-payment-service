namespace FCG.PaymentService.Api.Contracts;
public record CreatePaymentRequest(
    Guid? Id,    
    Guid OrderId,
    Guid UserId,
    decimal Amount,
    string Currency,
    string Status,
    string PaymentMethod,
    string Provider,
    DateTime CreatedAt,
    DateTime UpdatedAt
);

