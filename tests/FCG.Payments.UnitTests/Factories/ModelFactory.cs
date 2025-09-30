using FCG.Payments.Application.Contracts;

namespace FCG.Payments.UnitTests.Factories;
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
