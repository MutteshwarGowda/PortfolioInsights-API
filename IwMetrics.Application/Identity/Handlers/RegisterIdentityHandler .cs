
namespace IwMetrics.Application.Identity.Handlers
{
    public class RegisterIdentityHandler : IRequestHandler<RegisterIdentity, OperationResult<IdentityUserRegistrationDto>>
    {
        private readonly DataContext _ctx;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IdentityService _identityService;
        private readonly IMapper _mapper;
        private readonly RoleManager<IdentityRole> _roleManager;
        private OperationResult<IdentityUserRegistrationDto> _result = new();
        

        public RegisterIdentityHandler(DataContext ctx, UserManager<IdentityUser> userManager, IdentityService identityService, IMapper mapper,
                                       RoleManager<IdentityRole> roleManager)
        {
            _ctx = ctx;
            _userManager = userManager;
            _identityService = identityService;
            _mapper = mapper;
            _roleManager = roleManager;
        }

        public async Task<OperationResult<IdentityUserRegistrationDto>> Handle(RegisterIdentity request, CancellationToken cancellationToken)
        {
            try
            {
                await ValidateIdentityDoesNotExist(request);
                if (_result.IsError) return _result;

                await using var transaction = await _ctx.Database.BeginTransactionAsync(cancellationToken);

                var identity = await CreateIdentityUserAsync(request, transaction, cancellationToken);
                if (_result.IsError) return _result;

                var profile = await CreateUserProfileAsync(request, transaction, identity, cancellationToken);

                var roleAssignmentResult = await AssignRoleToUser(identity, "AppUser");
                if (!roleAssignmentResult.Succeeded)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    _result.AddError(ErrorCode.OperationFailed, "Failed to assign role to the user.");
                    return _result;
                }

                await transaction.CommitAsync(cancellationToken);

                await _identityService.AddDefaultClaim(identity, profile);

                _result.PayLoad = _mapper.Map<IdentityUserRegistrationDto>(profile);
                _result.PayLoad.UserName = identity.UserName;
               
                return _result;
            }
            catch (UserProfileNotValidException ex)
            {

                ex.ValidationErrors.ForEach(e => _result.AddError(ErrorCode.ValidationError, e));
            }
            catch (Exception e)
            {
                _result.AddUnknownError(e.Message);
            }

            return _result;
        }

        private async Task ValidateIdentityDoesNotExist(RegisterIdentity request)
        {
            var existingIdentity = await _userManager.FindByEmailAsync(request.UserName);

            if (existingIdentity != null)
                _result.AddError(ErrorCode.IdentityUserAlreadyExists, IdentityErrorMessages.IdentityUserAlreadyExists);
        }

        private async Task<IdentityUser> CreateIdentityUserAsync(RegisterIdentity request, IDbContextTransaction transaction, CancellationToken cancellationToken)
        {
            var identity = new IdentityUser { Email = request.UserName, UserName = request.UserName };

            var createdIdentity = await _userManager.CreateAsync(identity, request.Password);

            if (!createdIdentity.Succeeded)
            {
                await transaction.RollbackAsync(cancellationToken);

                foreach (var identityError in createdIdentity.Errors)
                {
                    _result.AddError(ErrorCode.IdentityCreationFailed, identityError.Description);
                }
            }

            return identity;
        }

        private async Task<UserProfile> CreateUserProfileAsync(RegisterIdentity request, IDbContextTransaction transaction,
                                                               IdentityUser identity, CancellationToken cancellationToken)
        {
            var profileInfo = BasicInfo.CreateBasicInfo(request.FirstName, request.LastName, request.UserName, request.Phone, request.CurrentCity);

            var profile = UserProfile.CreateUserProfile(identity.Id, profileInfo);
            try
            {
                _ctx.UserProfiles.Add(profile);
                await _ctx.SaveChangesAsync();
                return profile;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        private async Task<IdentityResult> AssignRoleToUser(IdentityUser user, string roleName)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                return IdentityResult.Failed(new IdentityError { Description = $"Role {roleName} does not exist." });
            }

            return await _userManager.AddToRoleAsync(user, roleName);
        }
    }
}