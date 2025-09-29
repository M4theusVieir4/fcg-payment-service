using FCG.PaymentService.Application.Contracts;

namespace FCG.Payment.UnitTests.Factories;
public class ModelFactory
{
    public CreatePaymentInput CreatePaymentInput => new(
        Id: Guid.NewGuid(),
        OrderId: Guid.NewGuid(),
        UserId: Guid.NewGuid(),
        Amount: 99.99m,
        Currency: "USD",
        Status: "Pending",
        PaymentMethod: "CreditCard",
        Provider: "Stripe",
        CreatedAt: DateTime.UtcNow,
        UpdatedAt: DateTime.UtcNow
    );
    
}
