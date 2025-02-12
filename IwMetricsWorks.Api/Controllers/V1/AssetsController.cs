
namespace IwMetricsWorks.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route(ApiRoutes.BaseRoute)]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AssetsController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AssetsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAssets()
        {
            var result = await _mediator.Send(new GetAllAssets());
            var mapped = _mapper.Map<List<AssetResponse>>(result.PayLoad);

            return result.IsError ? HandleErrorResponse(result.Errors) : Ok(mapped);
        }

        [HttpGet]
        [Route(ApiRoutes.Assets.IdRoute)]
        public async Task<IActionResult> GetAssetById(string id)
        {
            var assetId = Guid.Parse(id);
            var query = new GetAssetById { AssetId = assetId };

            var result = await _mediator.Send(query);
            var mapped = _mapper.Map<AssetResponse>(result.PayLoad);

            return result.IsError ? HandleErrorResponse(result.Errors) : Ok(mapped);
        }

        [HttpPost]
        [Route(ApiRoutes.Assets.CreateAsset)]
        [ValidateModel]
        public async Task<IActionResult> CreateAsset([FromBody] CreateAssetRequest createAsset)
        {
            var type = _mapper.Map<AssetType>(createAsset.Type);

            var command = new CreateAssetCommand
            {
                PortfolioId = createAsset.PortfolioId,
                Name = createAsset.Name,
                Type = type,
                Value = createAsset.Value,
            };

            var result = await _mediator.Send(command);
            var mapped = _mapper.Map<AssetResponse>(result.PayLoad);

            return result.IsError ? HandleErrorResponse(result.Errors)
                                           : CreatedAtAction(nameof(GetAssetById), new { id = mapped.AssetId }, mapped);
        }

        [HttpPatch]
        [ValidateModel]
        public async Task<IActionResult> UpdateAsset([FromBody] UpdateAssetRequest updateAsset)
        {
            var type = _mapper.Map<AssetType>(updateAsset.Type);

            var command = new UpdateAssetCommand
            {
                AssetId = updateAsset.AssetId,
                Type = type,
                Value = updateAsset.Value,
                Name = updateAsset.Name,
                PortfolioId = updateAsset.PortfolioId
            };

            var result = await _mediator.Send(command);
            var mapped = _mapper.Map<AssetResponse>(result.PayLoad);

            return result.IsError ? HandleErrorResponse(result.Errors) : Ok(mapped);
        }

        [HttpDelete]
        [Route(ApiRoutes.Assets.IdRoute)]
        public async Task<IActionResult> DeleteAsset(string id)
        {
            var assetId = Guid.Parse(id);
            var command = new DeleteAssetCommand { AssetId = assetId };

            var result = await _mediator.Send(command);

            return result.IsError ? HandleErrorResponse(result.Errors) : NoContent();
        }
    }
}
