
namespace IwMetrics.Application.Portfolios.CommandHandler
{
    public class UpdatePortfolioHandler : IRequestHandler<UpdatePortfolioCommand, OperationResult<Portfolio>>
    {
        private readonly DataContext _ctx;

        public UpdatePortfolioHandler(DataContext ctx)
        {
            _ctx = ctx;    
        }

        public async Task<OperationResult<Portfolio>> Handle(UpdatePortfolioCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<Portfolio>();

            try
            {
                var portfolio = await _ctx.Portfolios
                    .Include(p => p.Assets)
                    .FirstOrDefaultAsync(p => p.PortfolioId == request.PortfolioId, cancellationToken);

                if (portfolio is null)
                {
                    result.AddError(ErrorCode.NotFound, string.Format(PortfolioErrorMessage.NotFound, request.PortfolioId));
                    return result;
                }


                var userProfile = await _ctx.UserProfiles.FirstOrDefaultAsync(up => up.UserProfileId == portfolio.UserProfileId, cancellationToken);

                if (userProfile is null)
                {
                    result.AddError(ErrorCode.NotFound, string.Format(PortfolioErrorMessage.ManagerNotFound, portfolio.UserProfileId));
                    return result;
                }

                if (portfolio.UserProfileId != request.ManagerId)
                {
                    result.AddError(ErrorCode.ValidationError, PortfolioErrorMessage.ManagerUnmatched);
                    return result;
                }

                portfolio.UpdatePortfolio(request.Name, request.RiskLevel, userProfile);

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
