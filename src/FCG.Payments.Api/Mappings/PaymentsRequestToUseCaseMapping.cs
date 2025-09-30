using FCG.Payments.Api.Contracts;
using FCG.Payments.Application.Contracts;
using Riok.Mapperly.Abstractions;

namespace FCG.Payments.Api.Mappings;

[Mapper]
public static partial class PaymentsRequestToUseCaseMapping
{
    public static partial CreatePaymentInput ToUseCase(this CreatePaymentRequest request);
}
