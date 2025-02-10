namespace IwMetricsWorks.Api.Contracts.Asset.Request
{
    public record AssetRequest
    {
        public string Name { get; init; } // Asset name
        public decimal Value { get; init; } // Asset value
        public AssetTypeResponse Type { get; init; }
    }
}
