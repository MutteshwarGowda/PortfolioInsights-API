
namespace IwMetricsWorks.Api.MappingProfiles
{
    public class IdentityMappings : Profile
    {
        public IdentityMappings()
        {
            CreateMap<UserRegistration, RegisterIdentity>();
           
            CreateMap<Login, LoginCommand>();

            CreateMap<IdentityUserRegistrationDto, UserRegistrationResponse>();

            CreateMap<IdentityUserLoginDto, UserLoginResponse>();
        }
    }
}
