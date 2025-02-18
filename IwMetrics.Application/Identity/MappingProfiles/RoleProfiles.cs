
namespace IwMetrics.Application.Identity.MappingProfiles
{
    public class RoleProfiles : Profile
    {
        public RoleProfiles()
        {
            CreateMap<IdentityRole, IdentityRoleDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))   
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));    
        }
    }
}
