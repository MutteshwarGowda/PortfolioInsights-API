
namespace IwMetrics.Application.Portfolios.Queries
{
    public class GetPortfolioById : IRequest<OperationResult<Portfolio>>
    {
        public Guid PortfolioId { get; set; }
    }
}
