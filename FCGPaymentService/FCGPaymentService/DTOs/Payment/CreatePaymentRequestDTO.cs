namespace FCGPaymentService.API.DTOs.Payment;
public record CreatePaymentRequestDTO
{
    public Guid UserId { get; init; }
    public decimal Amount { get; init; }
}
