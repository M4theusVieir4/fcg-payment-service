using FCG.PaymentService.Application._Common;
using FCG.PaymentService.Application.Contracts;
using FCG.PaymentService.Domain;
using FCG.PaymentService.Domain._Common.Exceptions;

namespace FCG.PaymentService.Application.UseCases;
public class GetPaymentUseCase(IPaymentRepository paymentRepository) : IUseCase<GetPaymentInput, GetPaymentOutput>
{
    public async Task<GetPaymentOutput> Handle(GetPaymentInput input, CancellationToken ct)
    {
        var payment = await paymentRepository.GetByIdAsync(input.Id, ct: ct);

        if (payment is null) throw new FcgNotFoundException(input.Id, nameof(payment), $"Payment with id '{input.Id}' was not found.");

        return new GetPaymentOutput(payment);
    }
}
