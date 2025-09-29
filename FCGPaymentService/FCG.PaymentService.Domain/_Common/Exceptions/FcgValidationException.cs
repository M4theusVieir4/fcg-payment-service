namespace FCG.PaymentService.Domain._Common.Exceptions;
public class FcgValidationException(
    string field,
    string message
) : FcgException(message)
{
    public string Field { get; } = field;
}
