
namespace IwMetrics.Application.Assets.Queries
{
    public class GetAssetById : IRequest<OperationResult<Asset>>
    {
        public Guid AssetId { get; set; }
    }
}
