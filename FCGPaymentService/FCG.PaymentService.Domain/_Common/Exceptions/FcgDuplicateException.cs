namespace FCG.PaymentService.Domain._Common.Exceptions;
public class FcgDuplicateException(
    Guid? id,
    string entity,
    string message    
) : FcgException(message)
{
    public FcgDuplicateException(
        string entity,
        string message
    ) : this(null, entity, message) { }

    public Guid? Id { get; } = id;
    public string Entity { get; } = entity;
}

