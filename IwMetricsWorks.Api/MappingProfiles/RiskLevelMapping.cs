namespace IwMetricsWorks.Api.MappingProfiles
{
    public class RiskLevelMapping : Profile
    {
        public RiskLevelMapping()
        {
            CreateMap<RiskLevel, RiskLevelResponse>()
                .ForMember(dest => dest.Level, opt => opt.MapFrom(src => src.ToString()));

            CreateMap<RiskLevelResponse, RiskLevel>()
             .ConvertUsing(src => MapRiskLevel(src.Level));
        }

        private static RiskLevel MapRiskLevel(string level)
        {
            return Enum.TryParse<RiskLevel>(level, true, out var riskLevel) ? riskLevel : RiskLevel.Low;
        }
    }
}
