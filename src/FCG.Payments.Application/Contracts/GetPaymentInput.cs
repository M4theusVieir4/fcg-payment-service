using FCG.PaymentService.Application._Common;

namespace FCG.PaymentService.Application.Contracts;
public record GetPaymentInput(
    Guid Id
    ) : IUseCaseInput<GetPaymentOutput>;

