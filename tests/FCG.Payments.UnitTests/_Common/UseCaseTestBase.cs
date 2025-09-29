using NSubstituteAutoMocker;

namespace FCG.Payment.UnitTests._Common;
public abstract class UseCaseTestBase<TUseCase>(FcgFixture fixture) : TestBase(fixture)
    where TUseCase : class
{
    protected readonly NSubstituteAutoMocker<TUseCase> AutoMocker = new();
    protected TUseCase UseCase => AutoMocker.ClassUnderTest;
    protected TMock GetMock<TMock>() where TMock : class => AutoMocker.Get<TMock>();
}
