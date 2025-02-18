
namespace IwMetrics.Application.Identity.Handlers
{
    public class CreateRoleHandler : IRequestHandler<CreateRoleCommand, OperationResult<IdentityRoleDto>>
    {
        private readonly DataContext _ctx;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public CreateRoleHandler(DataContext ctx, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _ctx = ctx;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<OperationResult<IdentityRoleDto>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<IdentityRoleDto>();

            try
            {
                var roleExist = await _roleManager.RoleExistsAsync(request.Name);

                if (!roleExist) 
                {
                    var role = new IdentityRole(request.Name);
                    var roleResult = await _roleManager.CreateAsync(role);

                    if (roleResult.Succeeded)
                    {
                        result.PayLoad = _mapper.Map<IdentityRoleDto>(role);
                        return result;
                    }
                    else
                    {
                        result.AddError(ErrorCode.UnknownError, $"The Role {request.Name} has not been added");
                        return result;
                    }
                }

                result.AddError(ErrorCode.BadRequest, "Role already exist!");
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
