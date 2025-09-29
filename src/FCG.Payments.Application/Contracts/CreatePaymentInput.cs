using FCG.PaymentService.Application._Common;

namespace FCG.PaymentService.Application.Contracts;
public record CreatePaymentInput(
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
) : IUseCaseInput<CreatePaymentOutput>;
