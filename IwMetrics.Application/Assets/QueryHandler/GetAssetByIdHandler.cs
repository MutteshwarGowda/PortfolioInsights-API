
namespace IwMetrics.Application.Assets.QueryHandler
{
    public class GetAssetByIdHandler : IRequestHandler<GetAssetById, OperationResult<Asset>>
    {
        private readonly DataContext _ctx;

        public GetAssetByIdHandler(DataContext ctx)
        {
            _ctx = ctx;    
        }

        public async Task<OperationResult<Asset>> Handle(GetAssetById request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<Asset>();

            
            var asset = await _ctx.Assets.Include(a => a.Portfolio).FirstOrDefaultAsync(a => a.AssetId == request.AssetId, cancellationToken);

            if (asset is null)
            {
                result.AddError(ErrorCode.NotFound, string.Format(AssetErrorMessage.NotFound, request.AssetId));
                return result;
            }

            result.PayLoad = asset;
            return result;
        }
    }
}
