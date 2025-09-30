namespace FCG.Payments.Infra.Ioc.ElasticSearchConfig;
public class ElasticSearchSettings
{
    public required string Endpoint { get; init; }
    public required string AccessKey { get; init; }
    public required string Secret { get; init; }
    public required string IndexName { get; init; }
    public required string Region { get; init; }
}
