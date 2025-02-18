
namespace IwMetrics.Application.Identity.Commands
{
    public class RegisterIdentity : IRequest<OperationResult<IdentityUserRegistrationDto>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string CurrentCity { get; set; }
    }
}
