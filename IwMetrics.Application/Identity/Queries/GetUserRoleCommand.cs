
namespace IwMetrics.Application.Identity.Queries
{
    public class GetUserRoleCommand : IRequest<OperationResult<UserRoleDto>>
    {
        public string Email { get; set; }
    }
}
