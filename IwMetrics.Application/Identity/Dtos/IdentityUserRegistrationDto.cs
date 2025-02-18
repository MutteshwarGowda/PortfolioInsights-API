
namespace IwMetrics.Application.Identity.Dtos
{
    public class IdentityUserRegistrationDto
    {
        public Guid UserProfileId { get; private set; }
        public string IdentityId { get; private set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
