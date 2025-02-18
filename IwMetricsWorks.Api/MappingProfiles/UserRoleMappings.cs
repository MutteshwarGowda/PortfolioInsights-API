
namespace IwMetricsWorks.Api.MappingProfiles
{
    public class UserRoleMappings : Profile
    {
        public UserRoleMappings()
        {
             CreateMap<UserRoleDto, UserRoleResponse>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))   
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.RoleName));   
        }
    }
}
