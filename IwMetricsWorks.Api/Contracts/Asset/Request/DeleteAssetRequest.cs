using System.ComponentModel.DataAnnotations;

namespace IwMetricsWorks.Api.Contracts.Asset.Request
{
    public class DeleteAssetRequest
    {
        [Required]
        public Guid AssetId { get; init; } 
    }
}
