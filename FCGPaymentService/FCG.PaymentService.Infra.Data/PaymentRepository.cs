using FCG.PaymentService.Domain;
using OpenSearch.Client;

namespace FCG.PaymentService.Infra.Data;
public class PaymentRepository(IOpenSearchClient elasticClient) : IPaymentRepository
{
    public async Task<Payment?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        var response = await elasticClient.GetAsync<Payment>(id, ct: ct);

        return response.Found ? response.Source : null;
    }
}
