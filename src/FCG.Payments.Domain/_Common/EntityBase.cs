namespace FCG.PaymentService.Domain._Common;
public abstract class EntityBase(Guid? key = null)
{
    public Guid Id { get; protected set; } = key ?? Guid.NewGuid();
}
