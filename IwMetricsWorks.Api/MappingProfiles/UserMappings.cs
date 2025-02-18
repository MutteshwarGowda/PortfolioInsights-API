
namespace IwMetricsWorks.Api.MappingProfiles
{
    public class UserMappings : Profile
    {
        public UserMappings()
        {
            CreateMap<IdentityUserDto, UserResponse>();
                
        }
    }
}
