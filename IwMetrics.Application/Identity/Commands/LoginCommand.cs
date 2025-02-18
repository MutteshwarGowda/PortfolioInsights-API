
namespace IwMetrics.Application.Identity.Commands
{
    public class LoginCommand : IRequest<OperationResult<IdentityUserLoginDto>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
