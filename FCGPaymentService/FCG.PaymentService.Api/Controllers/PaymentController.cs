using FCG.PaymentService.Api.Mappings;
using FCG.PaymentService.Application.Contracts;
using FCGPaymentService.API.DTOs.Payment;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FCGPaymentService.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PaymentController(IMediator mediator) : ControllerBase
{
    [HttpGet("{id:guid}")]
    [ActionName(nameof(GetPaymentByIdAsync))]
    public async Task<IActionResult> GetPaymentByIdAsync([FromRoute] Guid id, CancellationToken ct)
    {
        var input = new GetPaymentInput(id);
        var output = await mediator.Send(input, ct);
        var response = output.ToResponse();        

        return Ok(response);
    }

    //[HttpPost]
    //public IActionResult CreatePayment([FromBody] CreatePaymentRequestDTO request)
    //{
    //    var payment = new PaymentResponseDTO
    //    {
    //        Id = Guid.NewGuid(),
    //        UserId = request.UserId,
    //        Amount = request.Amount,
    //        Status = "Pending"
    //    };

    //    return CreatedAtAction(nameof(GetPaymentById), new { id = payment.Id }, payment);
    //}


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

