using FCG.PaymentService.Application._Common;
using FCG.PaymentService.Application.Contracts;
using FCG.PaymentService.Domain;
using FCG.PaymentService.Domain._Common.Exceptions;

namespace FCG.PaymentService.Application.UseCases;
public class CreatePaymentUseCase(IPaymentRepository paymentRepository) : IUseCase<CreatePaymentInput, CreatePaymentOutput>
{
    public async Task<CreatePaymentOutput> Handle(CreatePaymentInput input, CancellationToken ct)
    {
        var duplicatedPayment = await paymentRepository.ExistByOrderIdAsync(input.OrderId, ct: ct);

        if (duplicatedPayment) throw new FcgDuplicateException(nameof(Payment), $"The order '{input.OrderId}' is already in use.");

        var payment = new Payment(
            input.Id,
            input.OrderId,
            input.UserId,
            input.Amount,
            input.Currency,
            input.Status,
            input.PaymentMethod,
            input.Provider,
            input.CreatedAt,
            input.UpdatedAt
        );

        await paymentRepository.IndexAsync(payment, ct);

        return new CreatePaymentOutput(payment);

    }
}
