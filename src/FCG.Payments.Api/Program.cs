using FCG.Payments.Api._Common;
using FCG.Payments.Api.Middlewares;
using FCG.Payments.Infra.Ioc;
using FCG.Payments.Infra.Ioc.ElasticSearchConfig;
using FCG.Payments.Infra.Ioc.Observability;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddEnvironmentVariables();

var authenticationSettings = builder.Configuration
    .GetSection("AuthenticationSettings")
    .Get<AuthenticationSettings>();

ArgumentNullException.ThrowIfNull(authenticationSettings);

var services = builder.Services;

 services.AddOpenTelemetryInfra(
    serviceName: "FCGPaymentService",
    serviceVersion: "1.0"
);

services.AddControllers();

services
    .AddEndpointsApiExplorer()
    .AddFcgPaymentsApiSwagger(authenticationSettings)
    .AddOpenApi()
    .AddInfrastructure(builder.Configuration);

services
    .ConfigureAuthentication(authenticationSettings)
    .ConfigureAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(c => {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "FCG Payment Service API");

        c.OAuthClientId(authenticationSettings.Audience);
        c.OAuthAppName("FCG Payment Service API - Swagger");
        c.OAuthUsePkce();
    });

    app.MapOpenApi();
}

app
    .UseMiddleware<ExceptionMiddleware>()
    .UseAuthentication()
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