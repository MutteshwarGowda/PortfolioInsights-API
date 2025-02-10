using IwMetrics.Application.Enums;
using IwMetrics.Application.Portfolios.Command;
using IwMetrics.Application.Portfolios.Queries;
using IwMetrics.DAL;
using IwMetricsWorks.Api.Contracts.Portfolio.Requests;
using IwMetricsWorks.Api.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace IwMetricsWorks.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route(ApiRoutes.BaseRoute)]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PortfolioController : BaseController
    {

        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public PortfolioController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPortfolios()
        {
            var result = await _mediator.Send( new GetAllPortfolio());
            var mapped = _mapper.Map<List<PortfolioResponse>>(result.PayLoad);
            return result.IsError ? HandleErrorResponse(result.Errors) : Ok(mapped);
        }

        
        [HttpGet]
        [Route(ApiRoutes.Portfolio.IdRoute)]
        public async Task<IActionResult> GetPortfolioById(string id)
        {
            var portfolioId = Guid.Parse(id);
            var query = new GetPortfolioById { PortfolioId = portfolioId };

            var result = await _mediator.Send( query );
            var mapped = _mapper.Map<PortfolioResponse>(result.PayLoad);

            return result.IsError ? HandleErrorResponse(result.Errors) : Ok(mapped);
        }

        [HttpPost]
        [Route(ApiRoutes.Portfolio.CreatePortfolio)]
        [ValidateModel]
        public async Task<IActionResult> CreatePortfolio([FromBody] PortfolioCreateRequest portfolioCreate)
        {
            var riskLevel = _mapper.Map<RiskLevel>(portfolioCreate.RiskLevel);

            var command = new CreatePortfolioCommand
            {
                Name = portfolioCreate.Name,
                ManagerId = portfolioCreate.ManagerId,
                RiskLevel = riskLevel
            };

            var result = await _mediator.Send(command);
            var mapped = _mapper.Map<PortfolioCreatedResponse>(result.PayLoad);

            return result.IsError ? HandleErrorResponse(result.Errors)
                                   : CreatedAtAction(nameof(GetPortfolioById), new { id = mapped.PortfolioId }, mapped);
        }

        [HttpPatch]
        [Route(ApiRoutes.Portfolio.IdRoute)]
        [ValidateModel]
        //[ValidateGuid("id")]
        public async Task<IActionResult> UpdatePortfolio([FromBody] PortfolioUpdateRequest portfolioUpdate)
        {
            var riskLevel = _mapper.Map<RiskLevel>(portfolioUpdate.RiskLevel);

            var command = new UpdatePortfolioCommand
            {
                Name = portfolioUpdate.Name,
                ManagerId = portfolioUpdate.ManagerId,
                RiskLevel = riskLevel,
                PortfolioId = portfolioUpdate.PortfolioId
            };

            var result = await _mediator.Send(command);
            var mapped = _mapper.Map<PortfolioResponse>(result.PayLoad);

            return result.IsError ? HandleErrorResponse(result.Errors) : Ok(mapped);
        }

        
        [HttpDelete]
        [Route(ApiRoutes.Portfolio.IdRoute)]
        //[ValidateGuid("id")]
        public async Task<IActionResult> DeletePortfolio([FromBody] PortfolioDeleteRequest portfolioDelete)
        {
            var command = new DeletePortfolioCommand
            {
                PortfolioId = portfolioDelete.PortfolioId,
                ManagerId = portfolioDelete.ManagerId

            };

            var result = await _mediator.Send(command);

            if (result.IsError)
            {
                if (result.Errors.Any(e => e.Code == ErrorCode.NotFound))
                    return NotFound(new { message = result.Errors.First().Message });

                // Return a 400 Bad Request for validation errors (e.g., Manager mismatch)
                return BadRequest(new { errors = result.Errors.Select(e => e.Message) });
            }

            return NoContent(); 

        }

    }
}
