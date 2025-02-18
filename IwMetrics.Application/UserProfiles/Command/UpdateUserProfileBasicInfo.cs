
namespace IwMetrics.Application.UserProfiles.Command
{
    public class UpdateUserProfileBasicInfo : IRequest<OperationResult<UserProfile>>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Phone { get; set; }
        public string CurrentCity { get; set; }
    }
}
