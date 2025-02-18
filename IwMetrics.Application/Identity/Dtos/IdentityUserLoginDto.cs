
namespace IwMetrics.Application.Identity.Dtos
{
    public class IdentityUserLoginDto
    {
        public string IdentityId { get; set; }
        public Guid? UserProfileId { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
        public DateTime TokenExpiration { get; set; }
    }
}
