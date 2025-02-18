
namespace IwMetrics.Application.Portfolios.QueryHandler
{
    public class GetPortfolioByIdHandler : IRequestHandler<GetPortfolioById, OperationResult<Portfolio>>
    {
        private readonly DataContext _ctx;

        public GetPortfolioByIdHandler(DataContext ctx)
        {
            _ctx = ctx;    
        }

        public async Task<OperationResult<Portfolio>> Handle(GetPortfolioById request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<Portfolio>();

            var portfolio = await _ctx.Portfolios.Include(p => p.Assets).Include(p => p.UserProfile).FirstOrDefaultAsync(p => p.PortfolioId == request.PortfolioId, cancellationToken);

            if (portfolio is null)
            {
                result.AddError(ErrorCode.NotFound, string.Format(PortfolioErrorMessage.NotFound, request.PortfolioId));
                return result;
            }

            result.PayLoad = portfolio;
            return result;
        }
    }
}
