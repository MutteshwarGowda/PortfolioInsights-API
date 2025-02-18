
namespace IwMetrics.Application.Assets.Command
{
    public class UpdateAssetCommand : IRequest<OperationResult<Asset>>
    {
        public Guid AssetId { get; set; }
        public string? Name { get; set; }
        public decimal? Value { get; set; }
        public AssetType? Type { get; set; }
        public Guid? PortfolioId { get; set; }
        public Guid? ManagerId { get; set; }
    }
}
