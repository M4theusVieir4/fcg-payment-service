namespace FCGPaymentService.API.DTOs.Admin;
public record WalletAdjustResponseDTO
{
    public Guid UserId { get; init; }
    public decimal Amount { get; init; }
    public string Type { get; init; } = string.Empty; 
    public string Reason { get; init; } = string.Empty;
    public DateTime Date { get; init; }
}
