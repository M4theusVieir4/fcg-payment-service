using FCG.Payments.Api.Contracts;
using FCG.Payments.Api.Mappings;
using FCG.Payments.Application.Contracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FCGPaymentService.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PaymentController(IMediator mediator) : ControllerBase
{
    [HttpGet("{id:guid}")]
    [ActionName(nameof(GetPaymentByIdAsync))]
    public async Task<ActionResult<GetPaymentResponse>> GetPaymentByIdAsync([FromRoute] Guid id, CancellationToken ct)
    {
        var input = new GetPaymentInput(id);
        var output = await mediator.Send(input, ct);
        var response = output.ToResponse();        

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<CreatePaymentResponse>> CreatePaymentAsync(
        [FromBody] CreatePaymentRequest request,
        CancellationToken ct)
    {
        var input = request.ToUseCase();
        var output = await mediator.Send(input, ct);
        var response = output.ToResponse();

        return CreatedAtAction(
            nameof(GetPaymentByIdAsync),
            new { id = response.Id },
            response
        );
    }


    //[HttpPost("{id}/confirm")]
    //public IActionResult ConfirmPayment(Guid id)
    //{
    //    var payment = new PaymentResponseDTO
    //    {
    //        Id = id,
    //        Status = "Confirmed"
    //    };

    //    return Ok(payment);
    //}

    //[HttpPost("{id}/cancel")]
    //public IActionResult CancelPayment(Guid id)
    //{
    //    var payment = new PaymentResponseDTO
    //    {
    //        Id = id,
    //        Status = "Cancelled"
    //    };

    //    return Ok(payment);
    //}

    //[HttpGet("user/{userId}")]
    //public IActionResult GetPaymentsByUser(Guid userId)
    //{        
    //    var payments = new List<PaymentResponseDTO>
    //        {
    //            new PaymentResponseDTO { Id = Guid.NewGuid(), UserId = userId, Amount = 100, Status = "Confirmed" },
    //            new PaymentResponseDTO { Id = Guid.NewGuid(), UserId = userId, Amount = 50, Status = "Pending" }
    //        };

    //    return Ok(payments);
    //}
}

