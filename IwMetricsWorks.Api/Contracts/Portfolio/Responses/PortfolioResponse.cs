
namespace IwMetricsWorks.Api.Contracts.Portfolio.Responses
{
    public record PortfolioResponse
    {
        public Guid PortfolioId { get; init; }
        public string Name { get; init; }
        public decimal TotalValue { get; set; }
        public Guid ManagerId { get; init; }
        public string? Manager { get; init; }
        public string RiskLevel { get; init; }
        public DateTime CreatedAt { get; init; }
        public DateTime? DateModified { get; init; }
        public List<AssetResponse> Assets { get; init; } 
    }
}
