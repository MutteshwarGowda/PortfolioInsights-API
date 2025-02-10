using System.ComponentModel.DataAnnotations;

namespace IwMetricsWorks.Api.Contracts.UserProfile.Requests
{
    public record UserProfileCreateRequest
    {
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string LastName { get; set; }

        [EmailAddress]
        [Required]
        public string EmailAddress { get; set; }

        [RegularExpression(@"^\d{10}$")]
        public string Phone { get; set; }

        public string CurrentCity { get; set; }
    }
}
