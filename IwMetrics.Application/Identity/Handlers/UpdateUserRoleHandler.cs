
namespace IwMetrics.Application.Identity.Handlers
{
    public class UpdateUserRoleHandler : IRequestHandler<UpdateUserRole, OperationResult<UserRoleDto>>
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UpdateUserRoleHandler(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<OperationResult<UserRoleDto>> Handle(UpdateUserRole request, CancellationToken cancellationToken)
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

                // Fetch roles assigned to the user
                var existingRoles = await _userManager.GetRolesAsync(user);

                // Remove all existing roles before assigning a new one
                if (existingRoles.Any())
                {
                    var removeResult = await _userManager.RemoveFromRolesAsync(user, existingRoles);
                    if (!removeResult.Succeeded)
                    {
                        result.AddError(ErrorCode.UnknownError, "Failed to remove existing roles from the user.");
                        return result;
                    }
                }

                // Assign the new role to the user
                var userRoleUpdate = await _userManager.AddToRoleAsync(user, request.RoleName);

                if (!userRoleUpdate.Succeeded)
                {
                    result.AddError(ErrorCode.UnknownError, $"Role was not updated to the requested user: {request.RoleName}");
                    return result;
                }

                // Remove previous claims 
                var userClaims = await _userManager.GetClaimsAsync(user);
                var internalRoleClaim = userClaims.FirstOrDefault(c => c.Type == "InternalRole");
                var externalRoleClaim = userClaims.FirstOrDefault(c => c.Type == "ExternalRole");

                if (internalRoleClaim != null)
                {
                    var removeInternalClaim = await _userManager.RemoveClaimAsync(user, internalRoleClaim);
                    if (!removeInternalClaim.Succeeded)
                    {
                        result.AddError(ErrorCode.UnknownError, "Failed to remove InternalRole claim.");
                        return result;
                    }
                }
                    

                if (externalRoleClaim != null)
                {
                    var removeExternalClaim = await _userManager.RemoveClaimAsync(user, externalRoleClaim);
                    if (!removeExternalClaim.Succeeded)
                    {
                        result.AddError(ErrorCode.UnknownError, "Failed to remove ExternalRole claim.");
                        return result;
                    }
                }
                    

                // Assign new claim based on role
                if (request.RoleName == "Admin" || request.RoleName == "PortfolioManager")
                {
                    var addInternalClaim = await _userManager.AddClaimAsync(user, new Claim("InternalRole", request.RoleName));
                    if (!addInternalClaim.Succeeded)
                    {
                        result.AddError(ErrorCode.UnknownError, "Failed to add InternalRole claim.");
                        return result;
                    }
                }
                else if (request.RoleName == "AppUser") 
                {
                    if (internalRoleClaim != null)
                        await _userManager.RemoveClaimAsync(user, internalRoleClaim);

                    var addExternalClaim = await _userManager.AddClaimAsync(user, new Claim("ExternalRole", "Client"));
                    if (!addExternalClaim.Succeeded)
                    {
                        result.AddError(ErrorCode.UnknownError, "Failed to add ExternalRole claim.");
                        return result;
                    }
                }

                // Fetch the updated role
                var updatedRoles = await _userManager.GetRolesAsync(user);

                result.PayLoad = new UserRoleDto
                {
                    Email = user.Email,
                    RoleName = updatedRoles.FirstOrDefault() 
                };

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
