
namespace IwMetrics.Application.UserProfiles.QueryHandler
{
    internal class GetAllUserProfileQueryHandler : IRequestHandler<GetAllUserProfile, OperationResult<IEnumerable<UserProfile>>>
    {
        private readonly DataContext _ctx;
        public GetAllUserProfileQueryHandler(DataContext ctx)
        {
            _ctx = ctx;    
        }

        public async Task<OperationResult<IEnumerable<UserProfile>>> Handle(GetAllUserProfile request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<IEnumerable<UserProfile>>();

            result.PayLoad = await _ctx.UserProfiles.ToListAsync();

            return result;
        }
    }
}
