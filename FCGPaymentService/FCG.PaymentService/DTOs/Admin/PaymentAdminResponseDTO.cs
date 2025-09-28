namespace FCGPaymentService.API.DTOs.Admin;
public record PaymentAdminResponseDTO
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public decimal Amount { get; init; }
    public string Status { get; init; } = string.Empty;
    public DateTime CreatedAt { get; init; }
}
