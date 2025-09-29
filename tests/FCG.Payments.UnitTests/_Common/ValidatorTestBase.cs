using FluentValidation;
using NSubstituteAutoMocker;

namespace FCG.Payment.UnitTests._Common;
public abstract class ValidatorTestBase<TValidator>(FcgFixture fixture) : TestBase(fixture)
    where TValidator : class, IValidator
{
    protected readonly NSubstituteAutoMocker<TValidator> AutoMocker = new();
    protected TValidator Validator => AutoMocker.ClassUnderTest;
}
