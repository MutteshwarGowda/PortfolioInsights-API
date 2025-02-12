using IwMetrics.Application.Identity.Commands;
using IwMetrics.Application.Identity.Dtos;

namespace IwMetricsWorks.Api.MappingProfiles
{
    public class IdentityMappings : Profile
    {
        public IdentityMappings()
        {
            CreateMap<UserRegistration, RegisterIdentity>();
            CreateMap<IdentityUserProfileDto, IdentityUserProfile>();
            CreateMap<Login, LoginCommand>();
        }
    }
}
