using FCGPaymentService.API.DTOs.Wallet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FCGPaymentService.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class WalletController : ControllerBase
{
    [HttpGet("{userId}")]
    public IActionResult GetWalletBalance(Guid userId)
    {
        var wallet = new WalletResponseDTO
        {
            UserId = userId,
            Balance = 150.75m
        };

        return Ok(wallet);
    }

    [HttpPost("{userId}/credit")]
    public IActionResult CreditWallet(Guid userId, [FromBody] WalletTransactionRequestDTO request)
    {
        var transaction = new WalletTransactionResponseDTO
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Amount = request.Amount,
            Type = "Credit",
            Date = DateTime.UtcNow,
            Description = request.Description
        };

        return CreatedAtAction(nameof(GetWalletTransactions), new { userId = userId }, transaction);
    }

    [HttpPost("{userId}/debit")]
    public IActionResult DebitWallet(Guid userId, [FromBody] WalletTransactionRequestDTO request)
    {
        var transaction = new WalletTransactionResponseDTO
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Amount = request.Amount,
            Type = "Debit",
            Date = DateTime.UtcNow,
            Description = request.Description
        };

        return CreatedAtAction(nameof(GetWalletTransactions), new { userId = userId }, transaction);
    }

    [HttpGet("{userId}/transactions")]
    public IActionResult GetWalletTransactions(Guid userId)
    {
        var transactions = new List<WalletTransactionResponseDTO>
            {
                new WalletTransactionResponseDTO { Id = Guid.NewGuid(), UserId = userId, Amount = 100, Type = "Credit", Date = DateTime.UtcNow.AddDays(-5), Description = "Gift card" },
                new WalletTransactionResponseDTO { Id = Guid.NewGuid(), UserId = userId, Amount = 50, Type = "Debit", Date = DateTime.UtcNow.AddDays(-2), Description = "Purchase" }
            };

        return Ok(transactions);
    }
}
