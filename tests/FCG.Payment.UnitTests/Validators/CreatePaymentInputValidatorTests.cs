using FCG.Payment.UnitTests._Common;
using FCG.PaymentService.Application.Contracts;
using FCG.PaymentService.Application.Validators;
using Shouldly;

namespace FCG.Payment.UnitTests.Validators;
public class CreatePaymentInputValidatorTests(FcgFixture fixture)
    : ValidatorTestBase<CreatePaymentInputValidator>(fixture)
{
    [Fact]
    public async Task ShouldAcceptInputAsync()
    {
        var input = ModelFactory.CreatePaymentInput;

        var result = await Validator.ValidateAsync(input);

        result.IsValid.ShouldBeTrue();
    }

    [Theory]
    [MemberData(nameof(InvalidInputs))]
    public async Task ShouldRejectInputAsync(CreatePaymentInput input)
    {
        var result = await Validator.ValidateAsync(input);

        result.IsValid.ShouldBeFalse();
    }

    public static TheoryData<CreatePaymentInput> InvalidInputs()
    {
        var input = ModelFactory.CreatePaymentInput; 

        return
        [
            input with { OrderId = Guid.Empty },                  
            input with { UserId = Guid.Empty },                   
            input with { Amount = 0 },                            
            input with { Amount = -10 },                          
            input with { Currency = default },                    
            input with { Currency = string.Empty },               
            input with { Currency = "US" },                       
            input with { Status = default },                      
            input with { Status = string.Empty },                 
            input with { Status = "Unknown" },                    
            input with { PaymentMethod = default },               
            input with { PaymentMethod = string.Empty },          
            input with { Provider = default },                    
            input with { Provider = string.Empty },               
            input with { CreatedAt = DateTime.UtcNow.AddDays(1) },
            input with { UpdatedAt = input.CreatedAt.AddDays(-1) },
        ];
    }
}
