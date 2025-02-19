
namespace IwMetrics.Application.Assets.CommandHandler
{
    public class UpdateAssetHandler : IRequestHandler<UpdateAssetCommand, OperationResult<Asset>>
    {
        private readonly DataContext _ctx;

        public UpdateAssetHandler(DataContext ctx)
        {
            _ctx = ctx;     
        }

        public async Task<OperationResult<Asset>> Handle(UpdateAssetCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<Asset>();

            try
            {
                var asset = await _ctx.Assets.Include(a => a.Portfolio).FirstOrDefaultAsync(a => a.AssetId == request.AssetId, cancellationToken);

                if (asset is null)
                {
                    result.AddError(ErrorCode.NotFound, string.Format(AssetErrorMessage.NotFound, request.AssetId));
                    return result;
                }

                if (asset.PortfolioId != request.PortfolioId)
                {
                    result.AddError(ErrorCode.ValidationError, PortfolioErrorMessage.PortfolioUpdateNotPossible);
                    return result;
                }

                if (asset.Portfolio.UserProfileId != request.ManagerId)
                {
                    result.AddError(ErrorCode.ValidationError, PortfolioErrorMessage.ManagerUnmatched);
                    return result;
                }

                var oldValue = asset.Value;

                asset.UpdateAsset(request.Name, request.Value, request.Type);

                if (request.Value.HasValue && request.Value != oldValue)
                {
                    asset.Portfolio.UpdateAssetValue(oldValue, request.Value.Value);  
                }

                await _ctx.SaveChangesAsync(cancellationToken);

                result.PayLoad = asset;
                return result;

            }
            catch (AssetNotValidException e)
            {

                e.ValidationErrors.ForEach(er => result.AddError(ErrorCode.ValidationError, er));
            }
            catch (Exception e)
            {
                result.AddUnknownError(e.Message);
            }

            return result;
        }
    }
}
