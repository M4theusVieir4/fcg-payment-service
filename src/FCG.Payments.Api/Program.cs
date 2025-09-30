using FCG.Payments.Api.Middlewares;
using FCG.Payments.Infra.Ioc;
using FCG.Payments.Infra.Ioc.Observability;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);
var appSettings = builder.Services;


builder.Services.AddOpenTelemetryInfra(
    serviceName: "FCGPaymentService",
    serviceVersion: "1.0"
);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthorization();

app.MapHealthChecks(
    "/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter
            .WriteHealthCheckUIResponse
    }
);

app.MapControllers();
app.Run();

public partial class Program { }