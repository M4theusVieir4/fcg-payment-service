using FCG.Payments.Api._Common.HealthChecks;
using FCG.Payments.Api.Middlewares;
using FCG.Payments.Infra.Ioc;
using FCG.Payments.Infra.Ioc.Observability;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddEnvironmentVariables();

var services = builder.Services;

 services.AddOpenTelemetryInfra(
    serviceName: "FCGPaymentService",
    serviceVersion: "1.0"
);

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddOpenApi();
services.AddInfrastructure(builder.Configuration);

services
    .AddHealthChecks()
    .AddCheck<OpenSearchHealthCheck>("opensearch");

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app
    .UseMiddleware<ExceptionMiddleware>()
    .UseAuthorization()
    .UseHttpMetrics();

app.MapHealthChecks(
    "/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter
            .WriteHealthCheckUIResponse
    }
);

app.MapMetrics("/metrics");

app.MapControllers();
app.Run();

public partial class Program { }