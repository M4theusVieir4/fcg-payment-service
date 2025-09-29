using FCG.PaymentService.Infra.Ioc;
using FCG.PaymentService.Infra.Ioc.Observability;

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
app.UseAuthorization();
app.MapControllers();
app.Run();
