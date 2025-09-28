namespace FCGPaymentService.API.DTOs.Refund;
public record RefundResponseDTO
{
    public Guid Id { get; init; }
    public Guid? UserId { get; init; }
    public Guid? PaymentId { get; init; }
    public decimal? Amount { get; init; }
    public string Status { get; init; } = string.Empty;
}
