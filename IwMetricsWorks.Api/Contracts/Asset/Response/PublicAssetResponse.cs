namespace IwMetricsWorks.Api.Contracts.Asset.Response
{
    public class PublicAssetResponse
    {
        public Guid AssetId { get; init; }
        public string Name { get; init; }
        public decimal Value { get; init; }
        public AssetType Type { get; init; }
    }
}
