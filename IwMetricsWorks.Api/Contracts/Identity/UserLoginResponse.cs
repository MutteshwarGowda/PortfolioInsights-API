namespace IwMetricsWorks.Api.Contracts.Identity
{
    public class UserLoginResponse
    {
        public string IdentityId { get; set; }  
        public Guid? UserProfileId { get; set; } 
        public string UserName { get; set; }
        public string Role { get; set; }  
        public string Token { get; set; } 
        public DateTime TokenExpiration { get; set; }
    }
}
