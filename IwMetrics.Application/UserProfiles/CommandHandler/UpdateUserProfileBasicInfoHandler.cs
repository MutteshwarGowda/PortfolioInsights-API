
namespace IwMetrics.Application.UserProfiles.CommandHandler
{
    internal class UpdateUserProfileBasicInfoHandler : IRequestHandler<UpdateUserProfileBasicInfo, OperationResult<UserProfile>>
    {
        private readonly DataContext _ctx;

        public UpdateUserProfileBasicInfoHandler(DataContext ctx)
        {
            _ctx = ctx;    
        }

        public async Task<OperationResult<UserProfile>> Handle(UpdateUserProfileBasicInfo request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<UserProfile>();

            try
            {
                var userProfile = await _ctx.UserProfiles.FirstOrDefaultAsync(up => up.UserProfileId == request.Id);

                if (userProfile == null)
                {
                    var identityUser = await _ctx.Users.FirstOrDefaultAsync(u => u.Id == request.Id.ToString());

                    if (identityUser == null)
                    {
                        result.IsError = true;
                        result.Errors.Add(new Error { Code = ErrorCode.NotFound, Message = $"User with IdentityId {request.Id} not found" });
                        return result;
                    }

                    var profileInfo = BasicInfo.CreateBasicInfo(request.FirstName, request.LastName, request.EmailAddress, request.Phone, request.CurrentCity);
                    var profile = UserProfile.CreateUserProfile(identityUser.Id, profileInfo);
                    
                    _ctx.UserProfiles.Add(profile);
                    await _ctx.SaveChangesAsync();

                    result.PayLoad = profile;
                    return result;
                   
                }

                userProfile.UpdateBasicInfo(userProfile.BasicInfo.WithUpdatedFields(request.FirstName, request.LastName, 
                    request.EmailAddress, request.Phone, request.CurrentCity));


                _ctx.UserProfiles.Update(userProfile);
                await _ctx.SaveChangesAsync();

                result.PayLoad = userProfile;
                return result;
            }
            catch (Exception e)
            {
                var error = new Error { Code = ErrorCode.ServerError, Message = e.Message };
                result.IsError = true;
                result.Errors.Add(error);
                return result;
            }
            

            return result;
        }
    }
}
