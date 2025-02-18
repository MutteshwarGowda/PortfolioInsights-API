
namespace IwMetrics.Application.Identity.QueryHandler
{
    public class GetUserRoleHandler : IRequestHandler<GetUserRoleCommand, OperationResult<UserRoleDto>>
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public GetUserRoleHandler(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;    
        }

        public async Task<OperationResult<UserRoleDto>> Handle(GetUserRoleCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<UserRoleDto>();

            try
            {
                var user = await _userManager.FindByEmailAsync(request.Email);

                if (user is null)
                {
                    result.AddError(ErrorCode.IdentityUserDoesNotExist, IdentityErrorMessages.IdentityUserDoesNotExist);
                    return result;
                }

                var role = await _userManager.GetRolesAsync(user);
                result.PayLoad = new UserRoleDto { Email = user.Email, RoleName = role.FirstOrDefault()};

            }
            catch (Exception e)
            {

                result.AddUnknownError(e.Message);
            }

            return result;
        }
    }
}
