﻿
namespace IwMetrics.Application.Portfolios.CommandHandler
{
    public class DeletePortfolioHandler : IRequestHandler<DeletePortfolioCommand, OperationResult<Portfolio>>
    {
        private readonly DataContext _ctx;

        public DeletePortfolioHandler(DataContext ctx)
        {
            _ctx = ctx;     
        }

        public async Task<OperationResult<Portfolio>> Handle(DeletePortfolioCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<Portfolio>();

            var portfolio = await _ctx.Portfolios.Include(p => p.Assets)
                                                 .FirstOrDefaultAsync(p => p.PortfolioId == request.PortfolioId, cancellationToken);

            if (portfolio is null)
            {
                result.AddError(ErrorCode.NotFound, string.Format(PortfolioErrorMessage.NotFound, request.PortfolioId));
                return result;
            }

            _ctx.Portfolios.Remove(portfolio);
            await _ctx.SaveChangesAsync(cancellationToken);

            result.PayLoad = portfolio;
            return result;
        }
    }
}
