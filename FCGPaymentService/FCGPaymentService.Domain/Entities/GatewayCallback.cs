namespace FCGPaymentService.Domain.Entities;
public class GatewayCallback
{
    public Guid CallbackId { get; set; }
    public string Provider { get; set; }
    public string EventType { get; set; }
    public string Payload { get; set; }
    public string Status { get; set; }
    public DateTime ReceivedAt { get; set; }
    public DateTime? ProcessedAt { get; set; }
}
