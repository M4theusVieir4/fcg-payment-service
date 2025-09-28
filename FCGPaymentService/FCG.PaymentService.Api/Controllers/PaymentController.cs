using FCGPaymentService.API.DTOs.Payment;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FCGPaymentService.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PaymentController : ControllerBase
{
    [HttpGet("{key:guid}")]
    [ActionName(nameof(GetPaymentByIdAsync))]
    public IActionResult GetPaymentByIdAsync([FromRoute] Guid key, CancellationToken ct)
    {
        var payment = new PaymentResponseDTO
        {
            Id = key,
            UserId = Guid.NewGuid(),
            Amount = 100,
            Status = "Pending"
        };

        return Ok(payment);
    }

    [HttpPost]
    public IActionResult CreatePayment([FromBody] CreatePaymentRequestDTO request)
    {
        var payment = new PaymentResponseDTO
        {
            Id = Guid.NewGuid(),
            UserId = request.UserId,
            Amount = request.Amount,
            Status = "Pending"
        };

        return CreatedAtAction(nameof(GetPaymentById), new { id = payment.Id }, payment);
    }


    [HttpPost("{id}/confirm")]
    public IActionResult ConfirmPayment(Guid id)
    {
        var payment = new PaymentResponseDTO
        {
            Id = id,
            Status = "Confirmed"
        };

        return Ok(payment);
    }

    [HttpPost("{id}/cancel")]
    public IActionResult CancelPayment(Guid id)
    {
        var payment = new PaymentResponseDTO
        {
            Id = id,
            Status = "Cancelled"
        };

        return Ok(payment);
    }

    [HttpGet("user/{userId}")]
    public IActionResult GetPaymentsByUser(Guid userId)
    {        
        var payments = new List<PaymentResponseDTO>
            {
                new PaymentResponseDTO { Id = Guid.NewGuid(), UserId = userId, Amount = 100, Status = "Confirmed" },
                new PaymentResponseDTO { Id = Guid.NewGuid(), UserId = userId, Amount = 50, Status = "Pending" }
            };

        return Ok(payments);
    }
}

