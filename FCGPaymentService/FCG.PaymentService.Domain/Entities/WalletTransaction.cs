namespace FCGPaymentService.Domain.Entities;
public class WalletTransaction
{
    public Guid TransactionId { get; set; }
    public Guid WalletId { get; set; }
    public string Type { get; set; } 
    public decimal Amount { get; set; }
    public string Source { get; set; }
    public string ReferenceId { get; set; }
    public string Status { get; set; }
    public DateTime CreatedAt { get; set; }
}
