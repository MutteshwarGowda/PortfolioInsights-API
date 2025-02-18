
namespace IwMetrics.Application.Assets.Command
{
    public class DeleteAssetCommand : IRequest<OperationResult<Asset>>
    {
        public Guid AssetId { get; init; }
    }
}
