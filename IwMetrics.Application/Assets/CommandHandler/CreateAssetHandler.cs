
namespace IwMetrics.Application.Assets.CommandHandler
{
    public class CreateAssetHandler : IRequestHandler<CreateAssetCommand, OperationResult<Asset>>
    {
        private readonly DataContext _ctx;

        public CreateAssetHandler(DataContext ctx)
        {
            _ctx = ctx;     
        }

        public async Task<OperationResult<Asset>> Handle(CreateAssetCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<Asset>();

            try
            {
                var portfolio = await _ctx.Portfolios.Include(p => p.Assets).FirstOrDefaultAsync(p => p.PortfolioId == request.PortfolioId, cancellationToken);

                if (portfolio == null)
                {
                    result.AddError(ErrorCode.NotFound, PortfolioErrorMessage.NotFound);
                    return result;
                }

                var asset = Asset.CreateAsset(request.Name, request.Value, request.Type, request.PortfolioId);
                
                portfolio.AddAsset(asset);

                _ctx.Assets.Add(asset);
                await _ctx.SaveChangesAsync(cancellationToken);

                result.PayLoad = asset;
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
