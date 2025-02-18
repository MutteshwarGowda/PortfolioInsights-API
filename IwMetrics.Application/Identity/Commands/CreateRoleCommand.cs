
namespace IwMetrics.Application.Identity.Commands
{
    public class CreateRoleCommand : IRequest<OperationResult<IdentityRoleDto>>
    {
        public string Name { get; set; }
    }
}
