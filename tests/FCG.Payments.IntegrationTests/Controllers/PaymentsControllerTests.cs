using Amazon.SQS;
using Amazon.SQS.Model;
using Elastic.Clients.Elasticsearch.MachineLearning;
using FCG.Payments.Api._Common;
using FCG.Payments.Api.Contracts;
using FCG.Payments.Domain;
using FCG.Payments.IntegrationTests._Common;
using Shouldly;
using System.Net;

namespace FCG.Payments.IntegrationTests.Controllers;
public class PaymentsControllerTests : ControllerTestBase
{
    private IPaymentRepository _paymentRepository;
    private IPaymentEventPublisher _paymentEventRepository;
    public PaymentsControllerTests(FcgFixture fixture) : base(fixture, "payment")
    {
        _paymentRepository = GetService<IPaymentRepository>();
        _paymentEventRepository = GetService<IPaymentEventPublisher>();
    }

    [Fact]
    public async Task ShouldCreatePaymentAsync()
    {
        var request = ModelFactory.CreatePaymentRequest
            with
        { Id = default };

        var (httpMessage, response) = await Requester.PostAsync<CreatePaymentResponse>(Uri, request, CancellationToken);

        httpMessage.StatusCode
            .ShouldBe(HttpStatusCode.Created);

        response.ShouldNotBeNull();
        response.Id.ShouldNotBe(Guid.Empty);
        response.OrderId.ShouldBe(request.OrderId);
        response.UserId.ShouldBe(request.UserId);
        response.Amount.ShouldBe(request.Amount);
        response.Currency.ShouldBe(request.Currency);        
        response.PaymentMethod.ShouldBe(request.PaymentMethod);
        response.Provider.ShouldBe(request.Provider);

        var payment = await _paymentRepository
            .GetByIdAsync(response.Id, CancellationToken);

        payment.ShouldNotBeNull();
        payment!.Id.ShouldBe(response.Id);
        payment.OrderId.ShouldBe(response.OrderId);
        payment.UserId.ShouldBe(response.UserId);
        payment.Amount.ShouldBe(response.Amount);
        payment.Currency.ShouldBe(response.Currency);
        payment.Status.ShouldBe(response.Status);
        payment.PaymentMethod.ShouldBe(response.PaymentMethod);
        payment.Provider.ShouldBe(response.Provider);

        var messages = await _paymentEventRepository.ListMessagesAsync(maxMessages: 10, CancellationToken);
        messages.ShouldContain(m => m.Id == response.Id);
    }

    [Fact]
    public async Task ShouldRejectRequestAsync()
    {
        var request = ModelFactory.CreatePaymentRequest
            with
        { Amount = 0 };

        var (httpMessage, response) = await Requester
            .PostAsync<ErrorResponse>(Uri, request, CancellationToken);

        httpMessage.StatusCode
            .ShouldBe(HttpStatusCode.BadRequest);
        response
            .ShouldNotBeNull();
        response.Errors
            .ShouldNotBeEmpty();

        var payment = await _paymentRepository
            .GetByIdAsync(request.Id.Value, CancellationToken);
        payment
            .ShouldBeNull();
    }

    [Fact]
    public async Task ShouldGetPaymentAsync()
    {
        var payment = EntityFactory.Payment;

        await _paymentRepository.IndexAsync(payment, CancellationToken);

        var (getHttpMessage, getResponse) = await Requester.GetAsync<CreatePaymentResponse>(
            new Uri($"{Uri}/{payment.Id}"),
            ct: CancellationToken
        );

        getHttpMessage.StatusCode
            .ShouldBe(HttpStatusCode.OK);
        getResponse
            .ShouldNotBeNull();
        getResponse.Id
            .ShouldBe(payment.Id);
        getResponse.OrderId
            .ShouldBe(payment.OrderId);
        getResponse.UserId
            .ShouldBe(payment.UserId);
        getResponse.Amount
            .ShouldBe(payment.Amount);
        getResponse.Currency
            .ShouldBe(payment.Currency);
        getResponse.Status
            .ShouldBe(payment.Status);
        getResponse.PaymentMethod
            .ShouldBe(payment.PaymentMethod);
        getResponse.Provider
            .ShouldBe(payment.Provider);
    }

    [Fact]
    public async Task ShouldReturnNotFoundForMissingPaymentAsync()
    {
        var missingId = Guid.NewGuid();
        var getUri = new Uri($"{Uri}/{missingId}");        
        var (httpMessage, response) = await Requester.GetAsync<ErrorResponse>(getUri, null, CancellationToken);

        httpMessage.StatusCode
            .ShouldBe(HttpStatusCode.NotFound);
        response
            .ShouldNotBeNull();
        response.Key
            .ShouldBe(missingId);
        response.Message
            .ShouldBe($"Payment with id '{missingId}' was not found.");
    }
}
