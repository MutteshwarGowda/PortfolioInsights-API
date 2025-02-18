
namespace IwMetricsWorks.Api.MappingProfiles
{
    public class UserProfileMappings : Profile
    {
        public UserProfileMappings()
        {
            CreateMap<UserProfileUpdateRequest, UpdateUserProfileBasicInfo>();
            CreateMap<BasicInfo, BasicInformation>();

            CreateMap<UserProfile, UserProfileResponse>()
                .ForMember(dest => dest.UserProfileId, opt => opt.MapFrom(src => src.UserProfileId))
                .ForMember(dest => dest.BasicInfo, opt => opt.MapFrom(src => src.BasicInfo))
                .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(src => src.DateCreated))
                .ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => src.LastModified));
               
            
        }
    }
}
