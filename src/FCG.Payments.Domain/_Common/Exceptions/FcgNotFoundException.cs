namespace FCG.PaymentService.Domain._Common.Exceptions;
public class FcgNotFoundException(
    Guid id,
    string entity,
    string message
) : FcgException(message)
{
    public Guid Id { get; } = id;
    public string Entity { get; } = entity;
}
