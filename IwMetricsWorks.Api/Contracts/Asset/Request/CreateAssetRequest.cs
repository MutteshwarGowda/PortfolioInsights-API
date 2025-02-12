using System.ComponentModel.DataAnnotations;

namespace IwMetricsWorks.Api.Contracts.Asset.Request
{
    public record CreateAssetRequest
    {
        [Required(ErrorMessage = "PortfolioId is Required")]
        public Guid PortfolioId { get; init; }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Name { get; init; }


        [Range(0.01, double.MaxValue, ErrorMessage = "Value must be greater than zero.")]
        public decimal Value { get; init; }


        [Required]
        public string Type { get; init; }

    }
}
