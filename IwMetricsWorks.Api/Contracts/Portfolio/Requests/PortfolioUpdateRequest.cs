namespace IwMetricsWorks.Api.Contracts.Portfolio.Requests
{
    public record PortfolioUpdateRequest
    {
        public Guid PortfolioId { get; init; } 
        public string? Name { get; init; } 
        public RiskLevelResponse? RiskLevel { get; init; } 
        public Guid ManagerId { get; init; } 
    }
}
