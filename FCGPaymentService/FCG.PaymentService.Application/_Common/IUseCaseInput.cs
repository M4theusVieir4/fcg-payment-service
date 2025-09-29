using MediatR;

namespace FCG.PaymentService.Application._Common;
public interface IUseCaseInput<out TOutput> : IRequest<TOutput> { }
public interface IUseCaseInput : IRequest { }
