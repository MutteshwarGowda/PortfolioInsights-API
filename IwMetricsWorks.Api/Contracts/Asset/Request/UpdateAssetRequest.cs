using System.ComponentModel.DataAnnotations;

namespace IwMetricsWorks.Api.Contracts.Asset.Request
{
    public record UpdateAssetRequest
    {
       
        [MinLength(3)]
        [MaxLength(50)]
        public string? Name { get; init; }



        [Range(0.01, double.MaxValue, ErrorMessage = "Value must be greater than zero.")]
        public decimal? Value { get; init; }    


        public string?  Type { get; init; }

        public Guid? PortfolioId { get; init; } 

    }
}
