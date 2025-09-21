namespace FCGPaymentService.API.DTOs.Wallet
{
    public class WalletTransactionResponseDTO
    {
        public Guid Id { get; init; }
        public Guid UserId { get; init; }
        public decimal Amount { get; init; }
        public string Type { get; init; } = string.Empty; 
        public DateTime Date { get; init; }
        public string Description { get; init; } = string.Empty;
    }
}
