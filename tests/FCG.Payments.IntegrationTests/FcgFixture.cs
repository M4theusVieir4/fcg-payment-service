using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace FCG.Payments.IntegrationTests;
public class FcgFixture : IDisposable
{
    public HttpClient Client { get; }
    public WebApplicationFactory<Program> Factory { get; }
    public CancellationToken CancellationToken { get; }
    private readonly CancellationTokenSource _cancellationTokenSource = new();
    private const string Environment = "TestAutomation";
    public FcgFixture()
    {
        CancellationToken = _cancellationTokenSource.Token;

        Factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder
                    .UseEnvironment(Environment)
                    .ConfigureAppConfiguration((_, config) =>
                    {
                        config
                            .AddJsonFile(
                                $"appsettings.{Environment}.json",
                                optional: false,
                                reloadOnChange: true
                            )
                            .AddEnvironmentVariables();
                    });
            });

        Client = Factory.CreateClient();
        Client.BaseAddress = new Uri("http://localhost:5237/api/");
    }

    public void Dispose()
    {
        _cancellationTokenSource.Cancel();
        Client.Dispose();
        Factory.Dispose();
    }
}
