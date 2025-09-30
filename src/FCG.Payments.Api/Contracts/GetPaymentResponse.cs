namespace FCG.Payments.Api.Contracts;
public record GetPaymentResponse(
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
);