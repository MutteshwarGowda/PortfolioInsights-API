
namespace IwMetrics.Application.Identity
{
    public class IdentityErrorMessages
    {
        public const string IdentityUserDoesNotExist = "Unable to find the User with the specified UserName";
        public const string IncorrectPassword = "The provided password is Incorrect";
        public const string IdentityUserAlreadyExists = "Provided adress already exist. Cannot register new User";
        public const string UnauthorizedAccountRemoval = "Cannot remove account as you are not its owner";
    }
}
