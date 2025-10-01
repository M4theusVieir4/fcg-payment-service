using FCG.Payments.Application._Common;
using FCG.Payments.Application.Contracts;
using FCG.Payments.Domain;
using FCG.Payments.Domain._Common.Exceptions;

namespace FCG.Payments.Application.UseCases;
public class CreatePaymentUseCase(
    IPaymentRepository paymentRepository, IPaymentEventPublisher eventPublisher) : IUseCase<CreatePaymentInput, CreatePaymentOutput>
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

        await eventPublisher.PublishPaymentCreatedAsync(payment, ct);

        return new CreatePaymentOutput(payment);

    }
}
