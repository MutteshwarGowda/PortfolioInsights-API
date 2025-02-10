using IwMetrics.Application.Assets.Command;
using IwMetrics.Application.Enums;
using IwMetrics.Application.Models;
using IwMetrics.Application.Portfolios;
using IwMetrics.DAL;
using IwMetrics.Domain.Aggregates.PortfolioAssets;
using IwMetrics.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                var asset = await _ctx.Assets.FirstOrDefaultAsync(a => a.AssetId == request.AssetId, cancellationToken);

                if (asset is null)
                {
                    result.AddError(ErrorCode.NotFound, string.Format(AssetErrorMessage.NotFound, request.AssetId));
                    return result;
                }

                var portfolio = await _ctx.Portfolios.FirstOrDefaultAsync(p => p.PortfolioId == asset.PortfolioId, cancellationToken);

                if (portfolio == null)
                {
                    result.AddError(ErrorCode.NotFound, string.Format(PortfolioErrorMessage.NotFound, request.PortfolioId));
                    return result;
                }

                if (asset.PortfolioId != request.PortfolioId)
                {
                    result.AddError(ErrorCode.ValidationError, PortfolioErrorMessage.PortfolioUpdateNotPossible);
                    return result;
                }

                //var portfolioManager = await _ctx.PortfolioManagers.FirstOrDefaultAsync(pm => pm.Id == portfolio.ManagerId, cancellationToken);

                //if (portfolioManager is null)
                //{
                //    result.AddError(ErrorCode.NotFound, string.Format(PortfolioErrorMessage.ManagerNotFound, request.ManagerId));
                //    return result;
                //}

                //if (portfolio.ManagerId != request.ManagerId)
                //{
                //    result.AddError(ErrorCode.ValidationError, ManagerErrorMessage.PortfolioManagerMismatch);
                //    return result;
                //}

                asset.UpdateAsset(request.Name, request.Value, request.Type);

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
