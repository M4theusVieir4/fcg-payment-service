using FCG.Payments.UnitTests._Common;
using FCG.Payments.Application.UseCases;
using FCG.Payments.Domain;
using FCG.Payments.Domain._Common.Exceptions;
using NSubstitute;
using Shouldly;

namespace FCG.Payments.UnitTests.UseCases;
public class CreatePaymentUseCaseTests : UseCaseTestBase<CreatePaymentUseCase>
{
    private IPaymentRepository _paymentRepository;
    private IPaymentEventPublisher _paymentEventPublisher;

    public CreatePaymentUseCaseTests(FcgFixture fixture) : base(fixture)
    {
        _paymentRepository = GetMock<IPaymentRepository>();
        _paymentEventPublisher = GetMock<IPaymentEventPublisher>();
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
                Arg.Any<Payment>(),
                CancellationToken
            );

        await _paymentEventPublisher.Received(1)
            .PublishPaymentCreatedAsync(Arg.Is<Payment>(p => p.Id == output.Id), CancellationToken);
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
                Arg.Any<Payment>(),
                CancellationToken
            );
    }
}
