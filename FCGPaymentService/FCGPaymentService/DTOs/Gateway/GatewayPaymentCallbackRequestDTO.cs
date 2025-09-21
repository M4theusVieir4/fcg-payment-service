namespace FCGPaymentService.API.DTOs.Gateway;
public record GatewayPaymentCallbackRequestDTO
{
    public Guid PaymentId { get; init; }
    public string Status { get; init; } = string.Empty; 
    public decimal Amount { get; init; }
    public DateTime Timestamp { get; init; }
    public string ProviderReference { get; init; } = string.Empty;
}
