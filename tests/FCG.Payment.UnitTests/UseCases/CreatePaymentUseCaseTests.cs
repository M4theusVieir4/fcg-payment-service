using FCG.Payment.UnitTests._Common;
using FCG.PaymentService.Application.UseCases;
using FCG.PaymentService.Domain;
using FCG.PaymentService.Domain._Common.Exceptions;
using NSubstitute;
using Shouldly;

namespace FCG.Payment.UnitTests.UseCases;
public class CreatePaymentUseCaseTests : UseCaseTestBase<CreatePaymentUseCase>
{
    private IPaymentRepository _paymentRepository;

    public CreatePaymentUseCaseTests(FcgFixture fixture) : base(fixture)
    {
        _paymentRepository = GetMock<IPaymentRepository>();
    }

    [Fact]
    public async Task ShouldCreatePaymentAsync()
    {
        var input = ModelFactory.CreatePaymentInput;

        var output = await UseCase.Handle(input, CancellationToken);

        output.ShouldNotBeNull();
        output.Id.ShouldNotBe(Guid.Empty);
        output.OrderId.ShouldBe(input.OrderId);
        output.UserId.ShouldBe(input.UserId);
        output.Amount.ShouldBe(input.Amount);
        output.Currency.ShouldBe(input.Currency);
        output.Status.ShouldBe(input.Status);
        output.PaymentMethod.ShouldBe(input.PaymentMethod);
        output.Provider.ShouldBe(input.Provider);
        output.CreatedAt.ShouldBe(input.CreatedAt);
        output.UpdatedAt.ShouldBe(input.UpdatedAt);

        await _paymentRepository
            .Received(1)
            .IndexAsync(
                Arg.Any<FCG.PaymentService.Domain.Payment>(),
                CancellationToken
            );
    }

    [Fact]
    public async Task ShouldThrowExceptionForPaymentDuplicationAsync()
    {
        var input = ModelFactory.CreatePaymentInput;

        _paymentRepository.ExistByOrderIdAsync(input.OrderId, ct: CancellationToken)
            .Returns(true);

        var duplicateException = await Should.ThrowAsync<FcgDuplicateException>(
            () => UseCase.Handle(input, CancellationToken)
         );

        duplicateException.Message
            .ShouldBe($"The order '{input.OrderId}' is already in use.");
        await _paymentRepository
            .DidNotReceive()
            .IndexAsync(
                Arg.Any<FCG.PaymentService.Domain.Payment>(),
                CancellationToken
            );
    }
}
