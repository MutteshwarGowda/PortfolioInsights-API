namespace IwMetricsWorks.Api.Contracts.Asset.Response
{
    public record AssetResponse
    {
        public Guid AssetId { get; init; }  
        public string Name { get; init; }   
        public decimal Value { get; init; } 
        public string Type { get; init; } // Type of the asset (Stock, Bond, Real Estate)
        public DateTime CreatedAt { get; init; }  
        public DateTime? DateModified { get; init; } 
        public Guid PortfolioId { get; init; } // Portfolio Id to which the asset belongs
        public string? PortfolioName { get; init; }
    }
}
