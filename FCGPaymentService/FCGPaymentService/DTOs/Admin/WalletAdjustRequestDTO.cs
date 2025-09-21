namespace FCGPaymentService.API.DTOs.Admin;
public class WalletAdjustRequestDTO
{
    public decimal Amount { get; init; }
    public string Reason { get; init; } = string.Empty;
}
