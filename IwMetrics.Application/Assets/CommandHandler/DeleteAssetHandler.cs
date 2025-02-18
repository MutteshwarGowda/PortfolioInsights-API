
namespace IwMetrics.Application.Assets.CommandHandler
{
    public class DeleteAssetHandler : IRequestHandler<DeleteAssetCommand, OperationResult<Asset>>
    {
        private readonly DataContext _ctx;

        public DeleteAssetHandler(DataContext ctx)
        {
            _ctx = ctx;    
        }

        public async Task<OperationResult<Asset>> Handle(DeleteAssetCommand request, CancellationToken cancellationToken)
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

                asset.Portfolio.RemoveAsset(asset);

                _ctx.Assets.Remove(asset);
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
