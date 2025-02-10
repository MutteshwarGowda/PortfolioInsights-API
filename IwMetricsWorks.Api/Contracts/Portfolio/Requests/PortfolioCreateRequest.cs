using IwMetricsWorks.Api.Contracts.Asset.Request;
using System.ComponentModel.DataAnnotations;

namespace IwMetricsWorks.Api.Contracts.Portfolio.Requests
{
    public record PortfolioCreateRequest
    {
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Name { get; init; } // Portfolio name


        [Required(ErrorMessage = "ManagerId is Required")]
        public Guid ManagerId { get; init; } // Portfolio Manager ID


        [Required]
        public RiskLevelResponse RiskLevel { get; init; } // Risk level of the portfolio

    }
}
