using Amazon;
using Amazon.Runtime;
using Amazon.SQS;
using FCG.Payments.Worker;
using OpenSearch.Client;

var builder = Host.CreateApplicationBuilder(args);

var sqsSettings = builder.Configuration.GetSection("AWS:SQS");
var region = RegionEndpoint.GetBySystemName(sqsSettings["Region"]);
var awsCredentials = new BasicAWSCredentials(
    sqsSettings["AccessKey"],
    sqsSettings["SecretKey"]
);

builder.Services.AddSingleton<IAmazonSQS>(sp =>
    new AmazonSQSClient(awsCredentials, region)
);

var elasticSearchSettings = builder.Configuration.GetSection("ElasticSearchSettings");
var settings = new ConnectionSettings(new Uri(elasticSearchSettings["Endpoint"]))
    .BasicAuthentication(elasticSearchSettings["AccessKey"], elasticSearchSettings["Secret"])
    .DefaultIndex(elasticSearchSettings["IndexName"]);

var client = new OpenSearchClient(settings);

builder.Services.AddSingleton<IOpenSearchClient>(client);
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
