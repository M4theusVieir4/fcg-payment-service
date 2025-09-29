using FCG.Payment.UnitTests.Factories;

namespace FCG.Payment.UnitTests._Common;
public class TestBase(FcgFixture fixture) : IClassFixture<FcgFixture>
{
    protected CancellationToken CancellationToken { get; } = fixture.CancellationToken;
    protected static ModelFactory ModelFactory { get; } = new();
}
