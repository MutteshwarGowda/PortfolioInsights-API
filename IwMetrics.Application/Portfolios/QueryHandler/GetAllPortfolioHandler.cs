
namespace IwMetrics.Application.Portfolios.QueryHandler
{
    public class GetAllPortfolioHandler : IRequestHandler<GetAllPortfolio, OperationResult<List<Portfolio>>>
    {
        private readonly DataContext _ctx;

        public GetAllPortfolioHandler(DataContext ctx)
        {
            _ctx = ctx;    
        }

        public async Task<OperationResult<List<Portfolio>>> Handle(GetAllPortfolio request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<List<Portfolio>>();

            try
            {
                var portfolios = await _ctx.Portfolios.Include(p => p.Assets).Include(p => p.UserProfile).ToListAsync(cancellationToken);
                result.PayLoad = portfolios;
                return result;
            }
            catch (Exception e)
            {

                result.AddUnknownError(e.Message);
            }

            return result;
        }
    }
}
