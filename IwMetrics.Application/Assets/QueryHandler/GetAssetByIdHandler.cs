using IwMetrics.Application.Assets.Queries;
using IwMetrics.Application.Enums;
using IwMetrics.Application.Models;
using IwMetrics.Infrastructure;
using IwMetrics.Domain.Aggregates.PortfolioAssets;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
