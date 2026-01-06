using FCG.Payments.IntegrationTests._Common;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
                    })
                    .ConfigureTestServices(services =>
                    {
                        services.AddAuthentication(options =>
                        {
                            options.DefaultAuthenticateScheme = "Test";
                            options.DefaultChallengeScheme = "Test";
                        })
                        .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("Test", options => { });
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
