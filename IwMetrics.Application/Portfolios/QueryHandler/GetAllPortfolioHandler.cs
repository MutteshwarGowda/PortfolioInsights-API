using IwMetrics.Application.Models;
using IwMetrics.Application.Portfolios.Queries;
using IwMetrics.Infrastructure;
using IwMetrics.Domain.Aggregates.PortfolioAssets;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IwMetrics.Application.Portfolios.QueryHandler
{
    public class GetAllPortfolioHandler : IRequestHandler<GetAllPortfolio, OperationResult<List<Portfolio>>>
    {
        private readonly DataContext _ctx;

        public GetAllPortfolioHandler(DataContext ctx)
        {
            _ctx = ctx;    
        }

        public async Task<OperationResult<List<Portfolio>>> Handle(GetAllPortfolio request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<List<Portfolio>>();

            try
            {
                var portfolios = await _ctx.Portfolios.Include(p => p.Assets).ToListAsync(cancellationToken);
                result.PayLoad = portfolios;
            }
            catch (Exception e)
            {

                result.AddUnknownError(e.Message);
            }

            return result;
        }
    }
}
