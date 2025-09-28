using FCGPaymentService.Domain.Entities;
using Nest;

namespace FCGPaymentService.Infra.Data.ElasticSearch;
public static class ElasticsearchIndexSetup
{
    public static async Task CreateIndicesAsync(IElasticClient client)
    {
        if (!(await client.Indices.ExistsAsync("payments")).Exists)
        {
            await client.Indices.CreateAsync("payments", c => c
                .Map(m => m.AutoMap<Payment>())
            );
        }

        if (!(await client.Indices.ExistsAsync("refunds")).Exists)
        {
            await client.Indices.CreateAsync("refunds", c => c
                .Map(m => m.AutoMap<Refund>())
            );
        }

        if (!(await client.Indices.ExistsAsync("wallets")).Exists)
        {
            await client.Indices.CreateAsync("wallets", c => c
                .Map(m => m.AutoMap<Wallet>())
            );
        }

        if (!(await client.Indices.ExistsAsync("wallet_transactions")).Exists)
        {
            await client.Indices.CreateAsync("wallet_transactions", c => c
                .Map(m => m.AutoMap<WalletTransaction>())
            );
        }

        if (!(await client.Indices.ExistsAsync("gateway_callbacks")).Exists)
        {
            await client.Indices.CreateAsync("gateway_callbacks", c => c
                .Map(m => m.AutoMap<GatewayCallback>())
            );
        }
    }
}

