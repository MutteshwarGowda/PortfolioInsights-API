﻿
namespace IwMetricsWorks.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route(ApiRoutes.BaseRoute)]
    [ApiController]
    public class IdentityController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public IdentityController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        [Route(ApiRoutes.Identity.Registration)]
        [ValidateModel]
        public async Task<IActionResult> Register(UserRegistration registration)
        {
            var command = _mapper.Map<RegisterIdentity>(registration);
            var result = await _mediator.Send(command);

            if (result.IsError) return HandleErrorResponse(result.Errors);

            return Ok(_mapper.Map<UserRegistrationResponse>(result.PayLoad));
        }

        [HttpPost]
        [Route(ApiRoutes.Identity.Login)]
        [ValidateModel]
        public async Task<IActionResult> Login(Login login)
        {
            var command = _mapper.Map<LoginCommand>(login);
            var result = await _mediator.Send(command);

            if (result.IsError) return HandleErrorResponse(result.Errors);
  
            return Ok(_mapper.Map<UserLoginResponse>(result.PayLoad));
        }
    }
}
