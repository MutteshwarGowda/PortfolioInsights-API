
namespace IwMetrics.Application.UserProfiles.CommandHandler
{
    internal class DeleteuserProfileHandler : IRequestHandler<DeleteUserProfileCommand, OperationResult<UserProfile>>
    {
        private readonly DataContext _ctx;

        public DeleteuserProfileHandler(DataContext ctx)
        {
            _ctx = ctx;     
        }
        public async Task<OperationResult<UserProfile>> Handle(DeleteUserProfileCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<UserProfile>();

            var userProfile = await _ctx.UserProfiles.FirstOrDefaultAsync(up => up.UserProfileId == request.UserProfileId, cancellationToken);

            if (userProfile is null)
            {
                result.AddError(ErrorCode.NotFound, $"No User Profile with Id {request.UserProfileId} found");
                return result;
            }

            _ctx.UserProfiles.Remove(userProfile);
            await _ctx.SaveChangesAsync(cancellationToken);

            result.PayLoad = userProfile;
            return result;
        }
    }
}
