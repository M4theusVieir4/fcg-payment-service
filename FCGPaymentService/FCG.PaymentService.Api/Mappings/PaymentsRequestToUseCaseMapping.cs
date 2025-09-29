using FCG.PaymentService.Api.Contracts;
using FCG.PaymentService.Application.Contracts;
using Riok.Mapperly.Abstractions;

namespace FCG.PaymentService.Api.Mappings;

[Mapper]
public static partial class PaymentsRequestToUseCaseMapping
{
    public static partial CreatePaymentInput ToUseCase(this CreatePaymentRequest request);
}
