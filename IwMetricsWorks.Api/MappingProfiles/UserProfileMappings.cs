
using IwMetrics.Domain.Aggregates.UserProfileAggregate;
using IwMetricsWorks.Api.Contracts.UserProfile.Responses;

namespace IwMetricsWorks.Api.MappingProfiles
{
    public class UserProfileMappings : Profile
    {
        public UserProfileMappings()
        {
            CreateMap<UserProfileCreateRequest, CreateUserCommand>();
            CreateMap<UserProfileUpdateRequest, UpdateUserProfileBasicInfo>();
            CreateMap<UserProfile, UserProfileResponse>();
            CreateMap<BasicInfo, BasicInformation>();
        }
    }
}
