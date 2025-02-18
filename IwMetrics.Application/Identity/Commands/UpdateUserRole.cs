
namespace IwMetrics.Application.Identity.Commands
{
    public class UpdateUserRole : IRequest<OperationResult<UserRoleDto>>
    {
        public string Email { get; set; }     // User Name
        public string RoleName { get; set; }
    }
}
