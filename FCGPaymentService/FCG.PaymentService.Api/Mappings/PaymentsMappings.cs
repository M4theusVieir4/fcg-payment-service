using FCG.PaymentService.Api.Contracts;
using FCG.PaymentService.Application.Contracts;
using Riok.Mapperly.Abstractions;

namespace FCG.PaymentService.Api.Mappings;
[Mapper]
public static partial class PaymentsMappings
{
    public static partial GetPaymentResponse ToResponse(this GetPaymentOutput output);
}
