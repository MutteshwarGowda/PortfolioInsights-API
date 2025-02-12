using IwMetrics.Application.Assets.Command;
using IwMetrics.Application.Enums;
using IwMetrics.Application.Models;
using IwMetrics.Application.Portfolios;
using IwMetrics.Infrastructure;
using IwMetrics.Domain.Aggregates.PortfolioAssets;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                var portfolioId = await _ctx.Portfolios.FirstOrDefaultAsync(p => p.PortfolioId == request.PortfolioId, cancellationToken);

                if (portfolioId == null)
                {
                    result.AddError(ErrorCode.NotFound, PortfolioErrorMessage.NotFound);
                    return result;
                }

                var asset = Asset.CreateAsset(request.Name, request.Value, request.Type, request.PortfolioId);

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
