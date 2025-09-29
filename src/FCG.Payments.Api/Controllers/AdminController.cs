//using FCGPaymentService.API.DTOs.Admin;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace FCGPaymentService.API.Controllers;
//[Route("api/[controller]")]
//[ApiController]
//public class AdminController : ControllerBase
//{
//    [HttpGet("payments")]
//    public IActionResult GetPayments([FromQuery] string? status, [FromQuery] Guid? userId)
//    {
//        var payments = new List<PaymentAdminResponseDTO>
//            {
//                new PaymentAdminResponseDTO { Id = Guid.NewGuid(), UserId = Guid.NewGuid(), Amount = 200, Status = "Confirmed", CreatedAt = DateTime.UtcNow.AddDays(-3) },
//                new PaymentAdminResponseDTO { Id = Guid.NewGuid(), UserId = Guid.NewGuid(), Amount = 150, Status = "Pending", CreatedAt = DateTime.UtcNow.AddDays(-1) }
//            };

//        if (!string.IsNullOrEmpty(status))
//            payments = payments.Where(p => p.Status.Equals(status, StringComparison.OrdinalIgnoreCase)).ToList();

//        if (userId.HasValue)
//            payments = payments.Where(p => p.UserId == userId).ToList();

//        return Ok(payments);
//    }

//    [HttpGet("refunds")]
//    public IActionResult GetRefunds([FromQuery] string? status, [FromQuery] Guid? userId)
//    {
//        var refunds = new List<RefundAdminResponseDTO>
//            {
//                new RefundAdminResponseDTO { Id = Guid.NewGuid(), UserId = Guid.NewGuid(), PaymentId = Guid.NewGuid(), Amount = 100, Status = "Approved", CreatedAt = DateTime.UtcNow.AddDays(-2) },
//                new RefundAdminResponseDTO { Id = Guid.NewGuid(), UserId = Guid.NewGuid(), PaymentId = Guid.NewGuid(), Amount = 50, Status = "Pending", CreatedAt = DateTime.UtcNow.AddDays(-1) }
//            };

//        if (!string.IsNullOrEmpty(status))
//            refunds = refunds.Where(r => r.Status.Equals(status, StringComparison.OrdinalIgnoreCase)).ToList();

//        if (userId.HasValue)
//            refunds = refunds.Where(r => r.UserId == userId).ToList();

//        return Ok(refunds);
//    }

//    [HttpPost("wallet/{userId}/adjust")]
//    public IActionResult AdjustWallet(Guid userId, [FromBody] WalletAdjustRequestDTO request)
//    {
//        var adjustment = new WalletAdjustResponseDTO
//        {
//            UserId = userId,
//            Amount = request.Amount,
//            Type = request.Amount >= 0 ? "Credit" : "Debit",
//            Reason = request.Reason,
//            Date = DateTime.UtcNow
//        };

//        return Ok(adjustment);
//    }
//}
