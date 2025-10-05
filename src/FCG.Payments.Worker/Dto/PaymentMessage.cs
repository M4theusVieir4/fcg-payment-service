namespace FCG.Payments.Worker.Dto;
public record PaymentMessage(
        Guid Id,
        Guid OrderId,
        Guid UserId,
        decimal Amount,
        string Currency,
        string Status,
        string PaymentMethod,
        string Provider,
        DateTime CreatedAt,
        DateTime UpdatedAt
);
