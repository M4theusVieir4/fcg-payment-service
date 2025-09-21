namespace FCGPaymentService.API.DTOs.Refund;
public record CreateRefundRequestDTO
{
    public Guid UserId { get; init; }
    public Guid PaymentId { get; init; }
    public decimal Amount { get; init; }
}
