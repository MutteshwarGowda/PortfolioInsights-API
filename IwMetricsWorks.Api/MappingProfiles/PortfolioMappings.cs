﻿
namespace IwMetricsWorks.Api.MappingProfiles
{
    public class PortfolioMappings : Profile
    {
        public PortfolioMappings()
        {
            CreateMap<Portfolio, PortfolioResponse>()
            .ForMember(dest => dest.PortfolioId, opt => opt.MapFrom(src => src.PortfolioId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.TotalValue, opt => opt.MapFrom(src => src.TotalValue))
            .ForMember(dest => dest.ManagerId, opt => opt.MapFrom(src => src.UserProfileId))
            .ForMember(dest => dest.Manager, opt => opt.MapFrom(src => $"{src.UserProfile.BasicInfo.FirstName} {src.UserProfile.BasicInfo.LastName}"))  // Assuming Manager has a Name property
            .ForMember(dest => dest.RiskLevel, opt => opt.MapFrom(src => src.RiskLevel))  
            .ForMember(dest => dest.Assets, opt => opt.MapFrom(src => src.Assets));

            CreateMap<Portfolio, PortfolioCreatedResponse>()
             .ForMember(dest => dest.PortfolioId, opt => opt.MapFrom(src => src.PortfolioId))
             .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
             .ForMember(dest => dest.ManagerId, opt => opt.MapFrom(src => src.UserProfileId))
             .ForMember(dest => dest.Manager, opt => opt.MapFrom(src => $"{src.UserProfile.BasicInfo.FirstName} {src.UserProfile.BasicInfo.LastName}"))
             .ForMember(dest => dest.RiskLevel, opt => opt.MapFrom(src => src.RiskLevel))
             .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt));


            CreateMap<PortfolioCreateRequest, CreatePortfolioCommand>()
             .ForMember(dest => dest.RiskLevel, opt => opt.MapFrom(src => src.RiskLevel))
             .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<PortfolioUpdateRequest, UpdatePortfolioCommand>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name)) // Map Name
            .ForMember(dest => dest.RiskLevel, opt => opt.MapFrom(src => src.RiskLevel));

        }

    }
}
