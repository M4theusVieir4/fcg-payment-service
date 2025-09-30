using MediatR;

namespace FCG.Payments.Application._Common;
public interface IUseCase<in TInput, TOutput>
    : IRequestHandler<TInput, TOutput>
    where TInput : IUseCaseInput<TOutput>
{
    new Task<TOutput> Handle(TInput input, CancellationToken ct);
}

public interface IUseCase<in TInput>
    : IRequestHandler<TInput>
    where TInput : IUseCaseInput
{
    new Task Handle(TInput input, CancellationToken ct);
}
