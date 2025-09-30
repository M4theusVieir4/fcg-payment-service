using FCG.Payments.Application.Contracts;
using FluentValidation;

namespace FCG.Payments.Application.Validators;
public class CreatePaymentInputValidator : AbstractValidator<CreatePaymentInput>
{
    public CreatePaymentInputValidator()
    {
        RuleFor(x => x.OrderId)
            .NotEmpty().WithMessage("OrderId é obrigatório.");

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId é obrigatório.");

        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Amount deve ser maior que zero.");

        RuleFor(x => x.Currency)
            .NotEmpty().WithMessage("Currency é obrigatório.")
            .Length(3).WithMessage("Currency deve ter 3 caracteres.");

        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("Status é obrigatório.")
            .Must(status => new[] { "Pending", "Completed", "Failed" }.Contains(status))
            .WithMessage("Status inválido.");

        RuleFor(x => x.PaymentMethod)
            .NotEmpty().WithMessage("PaymentMethod é obrigatório.");

        RuleFor(x => x.Provider)
            .NotEmpty().WithMessage("Provider é obrigatório.");       

        RuleFor(x => x.UpdatedAt)
            .GreaterThanOrEqualTo(x => x.CreatedAt)
            .WithMessage("UpdatedAt deve ser igual ou posterior a CreatedAt.");
    }
}
