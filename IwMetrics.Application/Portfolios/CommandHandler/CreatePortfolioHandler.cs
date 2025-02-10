using IwMetrics.Application.Enums;
using IwMetrics.Application.Models;
using IwMetrics.Application.Portfolios.Command;
using IwMetrics.Application.UserProfiles;
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

namespace IwMetrics.Application.Portfolios.CommandHandler
{
    public class CreatePortfolioHandler : IRequestHandler<CreatePortfolioCommand, OperationResult<Portfolio>>
    {
        private readonly DataContext _ctx;

        public CreatePortfolioHandler(DataContext ctx)
        {
            _ctx = ctx;    
        }

        public async Task<OperationResult<Portfolio>> Handle(CreatePortfolioCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<Portfolio>();

            try
            {
                var userProfile = await _ctx.UserProfiles.FirstOrDefaultAsync(up => up.UserProfileId == request.ManagerId, cancellationToken);

                if (userProfile is null)
                {
                    result.AddError(ErrorCode.NotFound, string.Format(UserProfileErrorMessage.NotFound, request.ManagerId));
                    return result;
                }


                var portfolio = Portfolio.CreateNew(request.Name, userProfile, request.RiskLevel);

                _ctx.Portfolios.Add(portfolio);
                await _ctx.SaveChangesAsync(cancellationToken);

                result.PayLoad = portfolio;
                return result;
            }
            catch (PortfolioNotValidException e)
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
