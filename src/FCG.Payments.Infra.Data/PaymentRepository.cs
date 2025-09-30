using FCG.Payments.Domain;
using OpenSearch.Client;

namespace FCG.Payments.Infra.Data;
public class PaymentRepository(IOpenSearchClient elasticClient) : IPaymentRepository
{
    public async Task<bool> ExistByOrderIdAsync(Guid orderId, Guid? ignoreId = null, CancellationToken ct = default)
    {
        var response = await elasticClient.SearchAsync<Payment>(s => s
            .Query(q => q
                .Bool(b => b
                    .Must(m => m
                        .Term(t => t.Field(f => f.OrderId).Value(orderId.ToString("D")))
                    )
                    .MustNot(mn => ignoreId.HasValue
                        ? mn.Term(t => t.Field(f => f.Id).Value(ignoreId.Value.ToString("D")))
                        : null
                    )
                )
            ), ct);

        return response.Documents.Any();
    }

    public async Task<Payment?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        var response = await elasticClient.GetAsync<Payment>(id, ct: ct);

        return response.Found ? response.Source : null;
    }

    public async Task IndexAsync(Payment payment, CancellationToken ct)
    {
        await elasticClient.IndexDocumentAsync(payment, ct);
    }
}
