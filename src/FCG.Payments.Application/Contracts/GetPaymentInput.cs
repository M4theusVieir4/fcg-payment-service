using FCG.Payments.Application._Common;

namespace FCG.Payments.Application.Contracts;
public record GetPaymentInput(
    Guid Id
    ) : IUseCaseInput<GetPaymentOutput>;

