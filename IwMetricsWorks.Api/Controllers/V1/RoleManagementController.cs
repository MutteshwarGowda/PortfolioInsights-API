using IwMetrics.Application.Identity.Queries;
using IwMetricsWorks.Api.Contracts.Roles;

namespace IwMetricsWorks.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route(ApiRoutes.BaseRoute)]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "RequireAdminUser")]
    public class RoleManagementController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public RoleManagementController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            var result = await _mediator.Send(new GetAllRoles());
            var mapped = _mapper.Map<List<RolesResponse>>(result.PayLoad);

            return (result.IsError) ? HandleErrorResponse(result.Errors) : Ok(mapped);
        }

        [HttpPost]
        [Route("CreateRole")]
        public async Task<IActionResult> CreateRoles(string name)
        {
            var command = new CreateRoleCommand { Name = name };
            var result = await _mediator.Send(command);
            var mapped = _mapper.Map<RolesResponse>(result.PayLoad);

            return (result.IsError) ? HandleErrorResponse(result.Errors) : Ok(mapped);
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var query = new GetAllUsers();
            var response = await _mediator.Send(query);
            var users = _mapper.Map<List<UserResponse>>(response.PayLoad);
            return Ok(users);
        }

        [HttpPatch]
        [Route("UpdateUserRole")]
        public async Task<IActionResult> UpdateUserRole(string email, string roleName)
        {
            var command = new UpdateUserRole { Email = email, RoleName = roleName };    

            var result = await _mediator.Send(command);
            var mapped = _mapper.Map<UserRoleResponse>(result.PayLoad);

            return (result.IsError) ? HandleErrorResponse(result.Errors) : Ok(mapped);
        }


        [HttpGet]
        [Route("GetUserRoles")]
        public async Task<IActionResult> GetUserRole(string email)
        {
            var query = new GetUserRoleCommand { Email = email };
            var result = await _mediator.Send(query);
            var mapped = _mapper.Map<UserRoleResponse>(result.PayLoad);

            return (result.IsError) ? HandleErrorResponse(result.Errors) : Ok(mapped);
        }

        [HttpDelete]
        [Route("RemoveUserFromRole")]
        public async Task<IActionResult> RemoveUserFromRole(string email, string roleName)
        {
            var command = new RemoveUserRoleCommand { Email = email, RoleName = roleName };
            var result = await _mediator.Send(command);
            

            return (result.IsError) ? HandleErrorResponse(result.Errors) : NoContent();
        }
    }
}
