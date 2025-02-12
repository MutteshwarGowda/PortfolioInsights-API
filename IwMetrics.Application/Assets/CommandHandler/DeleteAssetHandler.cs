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
                var asset = await _ctx.Assets.FirstOrDefaultAsync(a => a.AssetId == request.AssetId, cancellationToken);

                if (asset is null)
                {
                    result.AddError(ErrorCode.NotFound, string.Format(AssetErrorMessage.NotFound, request.AssetId));
                    return result;
                }

                var portfolio = await _ctx.Portfolios.FirstOrDefaultAsync(p => p.PortfolioId == asset.PortfolioId, cancellationToken);

                if (portfolio == null)
                {
                    result.AddError(ErrorCode.NotFound, string.Format(PortfolioErrorMessage.NotFound, asset.PortfolioId));
                    return result;
                }

                // Retrieve the logged-in user's ManagerId from the context (for example, JWT token)
                //var loggedInManagerId = GetLoggedInUserManagerId(); // Your method to get the logged-in user's manager ID

                // Check if the logged-in user is the manager of the portfolio
                //if (portfolio.ManagerId != loggedInManagerId)
                //{
                //    result.AddError(ErrorCode.ValidationError, "Logged-in user is not the manager of the portfolio.");
                //    return result;
                //}

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
