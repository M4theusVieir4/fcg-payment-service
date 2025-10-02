using Amazon;
using Amazon.Runtime;
using Amazon.SQS;
using FCG.Payments.Worker;

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

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
