
namespace IwMetrics.Application.Identity.Handlers
{
    public class RemoveUserRoleHandler : IRequestHandler<RemoveUserRoleCommand, OperationResult<UserRoleDto>>
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RemoveUserRoleHandler(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;  
            _roleManager = roleManager;
        }

        public async Task<OperationResult<UserRoleDto>> Handle(RemoveUserRoleCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<UserRoleDto>();

            try
            {
                var user = await _userManager.FindByEmailAsync(request.Email);

                if (user is null)
                {
                    result.AddError(ErrorCode.NotFound, $"User does not exist with email {request.Email}");
                    return result;
                }

                var roleExist = await _roleManager.RoleExistsAsync(request.RoleName);

                if (!roleExist)
                {
                    result.AddError(ErrorCode.NotFound, $"Role does not exist with name {request.RoleName}");
                    return result;
                }

                var removeUserRole = await _userManager.RemoveFromRoleAsync(user, request.RoleName);

                if (!removeUserRole.Succeeded)
                {
                    result.AddError(ErrorCode.OperationFailed, $"Failed to remove role {request.RoleName} for user {request.Email}");
                    return result;
                }


                // Remove associated claims
                var userClaims = await _userManager.GetClaimsAsync(user);

                if (request.RoleName == "Admin" || request.RoleName == "PortfolioManager")
                {
                    // Remove InternalRole claim
                    var internalRoleClaim = userClaims.FirstOrDefault(c => c.Type == "InternalRole");
                    if (internalRoleClaim != null)
                    {
                        await _userManager.RemoveClaimAsync(user, internalRoleClaim);
                    }

                    // Optional: If the user no longer has an internal role, consider assigning them back to "AppUser"
                    var remainingRoles = await _userManager.GetRolesAsync(user);
                    if (!remainingRoles.Any(r => r == "Admin" || r == "PortfolioManager"))
                    {
                        await _userManager.AddToRoleAsync(user, "AppUser");
                        await _userManager.AddClaimAsync(user, new Claim("ExternalRole", "Client"));
                    }
                }
                else if (request.RoleName == "AppUser")
                {
                    // Remove ExternalRole claim
                    var externalRoleClaim = userClaims.FirstOrDefault(c => c.Type == "ExternalRole");
                    if (externalRoleClaim != null)
                    {
                        await _userManager.RemoveClaimAsync(user, externalRoleClaim);
                    }
                }

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
