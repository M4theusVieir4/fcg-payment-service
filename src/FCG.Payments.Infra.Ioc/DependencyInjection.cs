using FCG.PaymentService.Application.UseCases;
using FCG.PaymentService.Application.Validators;
using FCG.PaymentService.Infra.Ioc.ElasticSearchConfig;
using FCG.PaymentService.Infra.Ioc.Pipelines;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FCG.PaymentService.Infra.Ioc;
public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var elasticSearchSettings = new ElasticSearchSettings
        {
            Endpoint = configuration["Elasticsearch:Uri"] ?? throw new ArgumentNullException("Elasticsearch:Uri"),
            AccessKey = Environment.GetEnvironmentVariable("AWS_ACCESS_KEY_ID")
                        ?? configuration["Elasticsearch:AccessKey"]
                        ?? throw new ArgumentNullException("Elasticsearch:AccessKey"),
            Secret = Environment.GetEnvironmentVariable("AWS_SECRET_ACCESS_KEY")
                        ?? configuration["Elasticsearch:SecretKey"]
                        ?? throw new ArgumentNullException("Elasticsearch:SecretKey"),
            Region = configuration["Elasticsearch:Region"] ?? throw new ArgumentNullException("Elasticsearch:Region"),
            IndexName = configuration["Elasticsearch:DefaultIndex"] ?? throw new ArgumentNullException("Elasticsearch:DefaultIndex")
        };

        services
            .AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetPaymentUseCase>())
            .AddValidatorsFromAssemblyContaining<CreatePaymentInputValidator>()
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(FluentValidatorPipeline<,>))
            .AddElasticSearchModule(elasticSearchSettings);
        //services.AddScoped<IPaymentRepository, ElasticsearchPaymentRepository>();
        //services.AddScoped<IRefundRepository, ElasticsearchRefundRepository>();
        //services.AddScoped<IWalletRepository, ElasticsearchWalletRepository>();
        //services.AddScoped<IWalletTransactionRepository, ElasticsearchWalletTransactionRepository>();
        //services.AddScoped<IGatewayCallbackRepository, ElasticsearchGatewayCallbackRepository>();        
    }
}
