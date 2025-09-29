//using FCGPaymentService.API.DTOs.Gateway;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace FCGPaymentService.API.Controllers;
//[Route("api/[controller]")]
//[ApiController]
//public class GatewayCallbackController : ControllerBase
//{
//    private const string GatewaySecret = "MEU_SECRET_GATEWAY"; 

//    [HttpPost("payment/{provider}")]
//    public IActionResult PaymentCallback(string provider, [FromBody] GatewayPaymentCallbackRequestDTO request)
//    {
//        if (!IsAuthorized())
//            return Unauthorized();

//        return Ok(new { message = $"Pagamento recebido do provedor {provider}" });
//    }

//    [HttpPost("refund/{provider}")]
//    public IActionResult RefundCallback(string provider, [FromBody] GatewayRefundCallbackRequestDTO request)
//    {
//        if (!IsAuthorized())
//            return Unauthorized();

//        return Ok(new { message = $"Reembolso recebido do provedor {provider}" });
//    }

//    private bool IsAuthorized()
//    {
//        if (Request.Headers.TryGetValue("X-Gateway-Secret", out var secret))
//        {
//            return secret == GatewaySecret;
//        }
//        return false;
//    }
//}
