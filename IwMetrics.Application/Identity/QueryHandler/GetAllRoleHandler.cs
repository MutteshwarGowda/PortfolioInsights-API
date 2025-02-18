
namespace IwMetrics.Application.Identity.QueryHandler
{
    public class GetAllRoleHandler : IRequestHandler<GetAllRoles, OperationResult<List<IdentityRoleDto>>>
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public GetAllRoleHandler(RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<OperationResult<List<IdentityRoleDto>>> Handle(GetAllRoles request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<List<IdentityRoleDto>>();

            try
            {
                var roles = await _roleManager.Roles.ToListAsync(cancellationToken);
                result.PayLoad = _mapper.Map<List<IdentityRoleDto>>(roles);
            }
            catch (Exception e)
            {

                result.AddUnknownError(e.Message);
            }

            return result;
        }
    }
}
