
namespace IwMetrics.Application.Identity.Handlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, OperationResult<IdentityUserLoginDto>>
    {
        private readonly DataContext _ctx;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IdentityService _identityService;
        private readonly IMapper _mapper;
        private OperationResult<IdentityUserLoginDto> _result = new();

        public LoginCommandHandler(DataContext ctx, UserManager<IdentityUser> userManager, IdentityService identityService, IMapper mapper)
        {
            _ctx = ctx;     
            _userManager = userManager;
            _identityService = identityService;
            _mapper = mapper;
        }

        public async Task<OperationResult<IdentityUserLoginDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            
            try
            {
                var identityUser = await ValidateAndGetIdentityAsync(request);

                if (_result.IsError) return _result;

                var userProfile = await _ctx.UserProfiles.FirstOrDefaultAsync(up => up.IdentityId == identityUser.Id);

                var claims = await _identityService.GetAllValidClaims(identityUser);
                var role = await _userManager.GetRolesAsync(identityUser);

                var jwtResponse = _identityService.GenerateJwtToken(claims);

                _result.PayLoad = new IdentityUserLoginDto
                {
                    IdentityId = identityUser.Id,
                    UserProfileId = userProfile?.UserProfileId,
                    UserName = identityUser?.UserName ?? string.Empty, 
                    Role = role.FirstOrDefault(),
                    Token = jwtResponse.Token,
                    TokenExpiration = jwtResponse.Expiration
                };

                return _result;
            }
            catch (Exception e)
            {

                _result.AddUnknownError(e.Message);
            }

            return _result;
        }

        private async Task<IdentityUser> ValidateAndGetIdentityAsync(LoginCommand request)
        {
            var identityUser = await _userManager.FindByEmailAsync(request.UserName);

            if (identityUser is null)
                _result.AddError(ErrorCode.IdentityUserDoesNotExist, IdentityErrorMessages.IdentityUserDoesNotExist);

            var validPassword = await _userManager.CheckPasswordAsync(identityUser, request.Password);

            if (!validPassword)
                _result.AddError(ErrorCode.IncorrectPassword, IdentityErrorMessages.IncorrectPassword);

            return identityUser;
        }

    }
}
