namespace IwMetricsWorks.Api.Contracts.Asset.Response
{
    public record AssetResponse
    {
        public Guid AssetId { get; init; }  // Unique identifier for the asset
        public string Name { get; init; }   // Name of the asset
        public decimal Value { get; init; } // Current value of the asset
        public string Type { get; init; } // Type of the asset (Stock, Bond, Real Estate)
        public DateTime CreatedAt { get; init; }  // Date when asset was created
        public DateTime? DateModified { get; init; } // Date when asset was last modified
        public Guid PortfolioId { get; init; } // Portfolio Id to which the asset belongs
        public string? PortfolioName { get; init; }
    }
}
