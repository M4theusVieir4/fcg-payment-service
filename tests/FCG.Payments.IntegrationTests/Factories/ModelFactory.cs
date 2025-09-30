using FCG.Payments.Api.Contracts;

namespace FCG.Payments.IntegrationTests.Factories;
public class ModelFactory
{
    public CreatePaymentRequest CreatePaymentRequest => new(
            Guid.NewGuid(),                              
            Guid.NewGuid(),                              
            Guid.NewGuid(),                              
            100.50m,                                     
            "USD",                                       
            "Pending",                                   
            "CreditCard",                                
            "Stripe",                                    
            DateTime.UtcNow,                             
            DateTime.UtcNow                              
        );
}
