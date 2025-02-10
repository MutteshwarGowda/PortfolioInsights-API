namespace IwMetricsWorks.Api.Contracts.Portfolio.Requests
{
    public class PortfolioDeleteRequest
    {
        public Guid PortfolioId { get; set; }
        public Guid ManagerId { get; set; }
    }
}
