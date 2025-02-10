using System.ComponentModel.DataAnnotations;

namespace IwMetricsWorks.Api.Contracts.UserProfile.Requests
{
    public class UserProfileUpdateRequest
    {
        [MinLength(3)]
        [MaxLength(50)]
        public string? FirstName { get; set; }

        [MinLength(3)]
        [MaxLength(50)]
        public string? LastName { get; set; }

        [EmailAddress]
        public string? EmailAddress { get; set; }

        public string? Phone { get; set; }

        public string? CurrentCity { get; set; }
    }
}
