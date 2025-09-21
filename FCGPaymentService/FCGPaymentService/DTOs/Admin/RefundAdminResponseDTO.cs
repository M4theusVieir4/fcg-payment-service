namespace FCGPaymentService.API.DTOs.Admin;
public record RefundAdminResponseDTO
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public Guid PaymentId { get; init; }
    public decimal Amount { get; init; }
    public string Status { get; init; } = string.Empty;
    public DateTime CreatedAt { get; init; }
}
