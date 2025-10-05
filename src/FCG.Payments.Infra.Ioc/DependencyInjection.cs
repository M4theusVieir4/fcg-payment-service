using Amazon;
using Amazon.Runtime;
using Amazon.SQS;
using FCG.Payments.Application.UseCases;
using FCG.Payments.Application.Validators;
using FCG.Payments.Domain;
using FCG.Payments.Infra.Ioc.ElasticSearchConfig;
using FCG.Payments.Infra.Ioc.HealthChecks;
using FCG.Payments.Infra.Ioc.Pipelines;
using FCG.Payments.Infra.Messaging;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FCG.Payments.Infra.Ioc;
public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var sqsSettings = configuration.GetSection("AWS:SQS");
        var region = RegionEndpoint.GetBySystemName(sqsSettings["Region"]);

        var elasticSearchSettings = configuration
            .Get<AppSettings>()?
            .ElasticSearchSettings;

        ArgumentNullException.ThrowIfNull(elasticSearchSettings);

        var awsCredentials = new BasicAWSCredentials(
            sqsSettings["AccessKey"],
            sqsSettings["SecretKey"]
        );

        services
            .AddRouting(options =>
            {
                options.LowercaseUrls = true;
            })
            .AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetPaymentUseCase>())
            .AddValidatorsFromAssemblyContaining<CreatePaymentInputValidator>()
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(FluentValidatorPipeline<,>))
            .AddElasticSearchModule(elasticSearchSettings)        
            .AddHealthChecks()
            .AddCheck<OpenSearchHealthCheck>("opensearch", tags: new[] { "search" });

        services.AddSingleton<IAmazonSQS>(sp =>
            new AmazonSQSClient(awsCredentials, region)
        );

        services.AddScoped<IPaymentEventPublisher>(sp =>
            new PaymentEventPublisher(
                sp.GetRequiredService<IAmazonSQS>(),
                configuration["AWS:SQS:PaymentsQueueUrl"]
                    ?? throw new ArgumentNullException("AWS:SQS:PaymentsQueueUrl")
            )
        );

    }
}
