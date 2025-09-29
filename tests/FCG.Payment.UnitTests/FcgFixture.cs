namespace FCG.Payment.UnitTests;
public class FcgFixture : IDisposable
{
    private readonly CancellationTokenSource _cancellationTokenSource = new();

    public FcgFixture()
    {
        CancellationToken = _cancellationTokenSource.Token;
    }

    public CancellationToken CancellationToken { get; private set; }

    public void Dispose() => _cancellationTokenSource.Cancel();    
}
