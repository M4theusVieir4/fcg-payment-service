using Amazon;
using Amazon.Runtime;
using Elasticsearch.Net;
using Elasticsearch.Net.Aws;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;

namespace FCG.PaymentService.Infra.Ioc;
public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var uri = new Uri(configuration["Elasticsearch:Uri"]);
        var accessKey = Environment.GetEnvironmentVariable("AWS_ACCESS_KEY_ID")
                        ?? configuration["Elasticsearch:AccessKey"];
        var secretKey = Environment.GetEnvironmentVariable("AWS_SECRET_ACCESS_KEY")
                        ?? configuration["Elasticsearch:SecretKey"];
        var region = configuration["Elasticsearch:Region"];
        var defaultIndex = configuration["Elasticsearch:DefaultIndex"];

        var awsCredentials = new BasicAWSCredentials(accessKey, secretKey);
        var regionEndpoint = RegionEndpoint.GetBySystemName(region);
        var pool = new SingleNodeConnectionPool(uri);
        
        var httpConnection = new AwsHttpConnection(awsCredentials, regionEndpoint);
        var settings = new ConnectionSettings(pool, httpConnection)
                            .DefaultIndex(defaultIndex)
                            .EnableApiVersioningHeader(false)
                            .RequestTimeout(TimeSpan.FromMinutes(2));

        var client = new ElasticClient(settings);

        services.AddSingleton<IElasticClient>(client);        
        //services.AddScoped<IPaymentRepository, ElasticsearchPaymentRepository>();
        //services.AddScoped<IRefundRepository, ElasticsearchRefundRepository>();
        //services.AddScoped<IWalletRepository, ElasticsearchWalletRepository>();
        //services.AddScoped<IWalletTransactionRepository, ElasticsearchWalletTransactionRepository>();
        //services.AddScoped<IGatewayCallbackRepository, ElasticsearchGatewayCallbackRepository>();        
    }
}
