namespace FCGPaymentService.Domain.Entities;
public class Wallet
{
    public Guid UserId { get; set; }
    public decimal Balance { get; set; }
    public string Currency { get; set; }
    public DateTime UpdatedAt { get; set; }
}
