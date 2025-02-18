
namespace IwMetrics.Application.Assets.Command
{
    public class CreateAssetCommand : IRequest<OperationResult<Asset>>
    {
        public string Name { get; set; }
        public decimal Value { get; set; }
        public AssetType Type { get; set; }
        public Guid PortfolioId { get; set; }
    }
}
