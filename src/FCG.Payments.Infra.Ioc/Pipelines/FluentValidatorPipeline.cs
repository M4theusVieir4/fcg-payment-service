using FCG.Payments.Domain._Common.Exceptions;
using FluentValidation;
using MediatR;

namespace FCG.Payments.Infra.Ioc.Pipelines;
public class FluentValidatorPipeline<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators = validators;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);
            var failures = _validators
                .Select(v => v.Validate(context))
                .SelectMany(result => result.Errors)
                .Where(f => f is not null)
                .ToArray();

            if (failures.Length is 0)
                return await next();

            var validationExceptions = failures
                .Select(f => new FcgValidationException(f.PropertyName, f.ErrorMessage))
                .ToArray();

            throw new FcgExceptionCollection(validationExceptions);
        }

        return await next();
    }
}
