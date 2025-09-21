namespace FCGPaymentService.API.DTOs.Payment;
public record PaymentResponseDTO
{
    public Guid Id { get; init; }
    public Guid? UserId { get; init; }
    public decimal? Amount { get; init; }
    public string Status { get; init; } = string.Empty;
}
