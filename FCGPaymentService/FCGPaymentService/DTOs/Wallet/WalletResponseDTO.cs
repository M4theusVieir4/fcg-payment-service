namespace FCGPaymentService.API.DTOs.Wallet;
public record WalletResponseDTO
{
    public Guid UserId { get; init; }
    public decimal Balance { get; init; }
}
