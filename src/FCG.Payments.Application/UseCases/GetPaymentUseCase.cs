using FCG.Payments.Application._Common;
using FCG.Payments.Application.Contracts;
using FCG.Payments.Domain;
using FCG.Payments.Domain._Common.Exceptions;

namespace FCG.Payments.Application.UseCases;
public class GetPaymentUseCase(IPaymentRepository paymentRepository) : IUseCase<GetPaymentInput, GetPaymentOutput>
{
    public async Task<GetPaymentOutput> Handle(GetPaymentInput input, CancellationToken ct)
    {
        var payment = await paymentRepository.GetByIdAsync(input.Id, ct: ct);

        if (payment is null) throw new FcgNotFoundException(input.Id, nameof(payment), $"Payment with id '{input.Id}' was not found.");

        return new GetPaymentOutput(payment);
    }
}
