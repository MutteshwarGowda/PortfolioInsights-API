namespace IwMetricsWorks.Api.Contracts.Portfolio.Responses
{
    public class PortfolioCreatedResponse
    {
        public Guid PortfolioId { get; init; }
        public string Name { get; init; }
        public Guid ManagerId { get; init; }
        public string? Manager { get; init; }
        public string RiskLevel { get; init; }
        public DateTime CreatedAt { get; init; }
    }
}
