
namespace IwMetricsWorks.Api.MappingProfiles
{
    public class Rolemappings : Profile
    {
        public Rolemappings()
        {
            CreateMap<IdentityRoleDto, RolesResponse>();    
        }
    }
}
