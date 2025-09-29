namespace FCG.PaymentService.Domain._Common;
public abstract class EntityBase(Guid? key = null)
{
    public Guid Key { get; protected set; } = key ?? Guid.NewGuid();
}
