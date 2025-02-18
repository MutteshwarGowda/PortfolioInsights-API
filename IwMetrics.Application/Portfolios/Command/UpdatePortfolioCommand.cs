
namespace IwMetrics.Application.Portfolios.Command
{
    public class UpdatePortfolioCommand : IRequest<OperationResult<Portfolio>>
    {
        public Guid PortfolioId { get; set; } 
        public string? Name { get; set; } 
        public RiskLevel? RiskLevel { get; set; } 
        public Guid ManagerId { get; set; }
       
    }
}
