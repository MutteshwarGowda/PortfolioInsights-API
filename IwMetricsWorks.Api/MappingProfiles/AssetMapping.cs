using IwMetricsWorks.Api.Contracts.Asset.Request;

namespace IwMetricsWorks.Api.MappingProfiles
{
    public class AssetMapping : Profile
    {
        public AssetMapping()
        {
            CreateMap<Asset, AssetResponse>()
                .ForMember(dest => dest.AssetId, opt => opt.MapFrom(src => src.AssetId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.PortfolioId, opt => opt.MapFrom(src => src.PortfolioId))
                .ForMember(dest => dest.PortfolioName, opt => opt.MapFrom(src => src.Portfolio.Name)) // Map portfolio name
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.DateModified, opt => opt.MapFrom(src => src.DateModified));

        }
    }
}
