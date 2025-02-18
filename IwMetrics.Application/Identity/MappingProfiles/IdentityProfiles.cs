
namespace IwMetrics.Application.Identity.MapingProfiles
{
    public class IdentityProfiles : Profile
    {
        public IdentityProfiles()
        {
            CreateMap<UserProfile, IdentityUserRegistrationDto>()
                .ForMember(dest => dest.UserProfileId, opt => opt.MapFrom(src => src.UserProfileId))
                .ForMember(dest => dest.IdentityId, opt => opt.MapFrom(src => src.IdentityId))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.BasicInfo.EmailAddress))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.BasicInfo.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.BasicInfo.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.BasicInfo.EmailAddress))
                .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(src => src.DateCreated));

            CreateMap<UserProfile, IdentityUserLoginDto>()
                .ForMember(dest => dest.IdentityId, opt => opt.MapFrom(src => src.IdentityId))
                .ForMember(dest => dest.UserProfileId, opt => opt.MapFrom(src => src.UserProfileId))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.BasicInfo.EmailAddress));
               
        }
    }
}