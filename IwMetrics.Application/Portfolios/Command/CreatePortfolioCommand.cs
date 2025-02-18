
namespace IwMetrics.Application.Portfolios.Command
{
    public class CreatePortfolioCommand : IRequest<OperationResult<Portfolio>>
    {
        public string Name { get; set; }
        public Guid ManagerId { get; set; }
        public RiskLevel RiskLevel { get; set; }
        
   
    }
}
