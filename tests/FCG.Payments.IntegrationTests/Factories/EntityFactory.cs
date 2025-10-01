using FCG.Payments.Domain;

namespace FCG.Payments.IntegrationTests.Factories;
public class EntityFactory
{
    public Payment Payment => new(
        id: Guid.NewGuid(),
        orderId: Guid.NewGuid(),
        userId: Guid.NewGuid(),
        amount: 99.99m,
        currency: "USD",
        status: "Pending",
        paymentMethod: "CreditCard",
        provider: "Stripe",
        createdAt: DateTime.UtcNow,
        updatedAt: DateTime.UtcNow
    );
}
