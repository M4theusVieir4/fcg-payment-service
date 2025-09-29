namespace FCG.PaymentService.Api.Contracts;
public record GetPaymentResponse(
   Guid PaymentId,
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