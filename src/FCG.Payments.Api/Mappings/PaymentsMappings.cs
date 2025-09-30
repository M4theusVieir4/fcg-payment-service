using FCG.Payments.Api.Contracts;
using FCG.Payments.Application.Contracts;
using Riok.Mapperly.Abstractions;

namespace FCG.Payments.Api.Mappings;
[Mapper]
public static partial class PaymentsMappings
{
    public static partial GetPaymentResponse ToResponse(this GetPaymentOutput output);
    public static partial CreatePaymentResponse ToResponse(this CreatePaymentOutput output);
}
