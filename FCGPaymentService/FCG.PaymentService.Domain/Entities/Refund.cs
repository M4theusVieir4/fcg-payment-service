namespace FCGPaymentService.Domain.Entities;
public class Refund
{
    public Guid RefundId { get; set; }
    public Guid PaymentId { get; set; }
    public Guid UserId { get; set; }
    public decimal Amount { get; set; }
    public string Status { get; set; }
    public string Reason { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
