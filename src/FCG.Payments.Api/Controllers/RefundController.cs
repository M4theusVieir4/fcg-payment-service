//using FCGPaymentService.API.DTOs.Refund;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace FCGPaymentService.API.Controllers;
//[Route("api/[controller]")]
//[ApiController]
//public class RefundController : ControllerBase
//{
//    [HttpPost]
//    public IActionResult RequestRefund([FromBody] CreateRefundRequestDTO request)
//    {
//        var refund = new RefundResponseDTO
//        {
//            Id = Guid.NewGuid(),
//            UserId = request.UserId,
//            PaymentId = request.PaymentId,
//            Amount = request.Amount,
//            Status = "Pending"
//        };

//        return CreatedAtAction(nameof(GetRefundById), new { id = refund.Id }, refund);
//    }

//    [HttpGet("{id}")]
//    public IActionResult GetRefundById(Guid id)
//    {
//        var refund = new RefundResponseDTO
//        {
//            Id = id,
//            UserId = Guid.NewGuid(),
//            PaymentId = Guid.NewGuid(),
//            Amount = 100,
//            Status = "Pending"
//        };

//        return Ok(refund);
//    }

//    [HttpGet("user/{userId}")]
//    public IActionResult GetRefundsByUser(Guid userId)
//    {
//        var refunds = new List<RefundResponseDTO>
//            {
//                new RefundResponseDTO { Id = Guid.NewGuid(), UserId = userId, PaymentId = Guid.NewGuid(), Amount = 100, Status = "Approved" },
//                new RefundResponseDTO { Id = Guid.NewGuid(), UserId = userId, PaymentId = Guid.NewGuid(), Amount = 50, Status = "Pending" }
//            };

//        return Ok(refunds);
//    }
//}
