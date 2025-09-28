namespace FCGPaymentService.API.DTOs.Wallet;
public record WalletTransactionRequestDTO
{
    public decimal Amount { get; init; }
    public string Description { get; init; } = string.Empty;
}
