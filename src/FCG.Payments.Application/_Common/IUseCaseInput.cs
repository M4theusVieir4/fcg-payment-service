using MediatR;

namespace FCG.Payments.Application._Common;
public interface IUseCaseInput<out TOutput> : IRequest<TOutput> { }
public interface IUseCaseInput : IRequest { }
