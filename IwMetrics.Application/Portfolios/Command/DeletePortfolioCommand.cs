
namespace IwMetrics.Application.Portfolios.Command
{
    public class DeletePortfolioCommand : IRequest<OperationResult<Portfolio>>
    {
        public Guid PortfolioId { get; set; } 
        public Guid ManagerId { get; set; }
    }
}
