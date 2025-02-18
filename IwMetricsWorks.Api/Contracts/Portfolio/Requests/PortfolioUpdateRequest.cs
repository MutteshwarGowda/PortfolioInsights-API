namespace IwMetricsWorks.Api.Contracts.Portfolio.Requests
{
    public record PortfolioUpdateRequest
    {
        public string? Name { get; init; } 
        public string? RiskLevel { get; init; } 
    }
}
