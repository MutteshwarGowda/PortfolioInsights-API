﻿
namespace IwMetrics.Application.Assets.QueryHandler
{
    public class GetAllAssetsHandler : IRequestHandler<GetAllAssets, OperationResult<List<Asset>>>
    {
        private readonly DataContext _ctx;

        public GetAllAssetsHandler(DataContext ctx)
        {
            _ctx = ctx;     
        }

        public async Task<OperationResult<List<Asset>>> Handle(GetAllAssets request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<List<Asset>>();

            try
            {
                var assets = await _ctx.Assets.Include(a => a.Portfolio).ToListAsync(cancellationToken);
                result.PayLoad = assets;
            }
            catch (Exception e)
            {

                result.AddUnknownError(e.Message);
            }

            return result;
        }
    }
}
