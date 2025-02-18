
namespace IwMetricsWorks.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route(ApiRoutes.BaseRoute)]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserProfileController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UserProfileController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Policy = "RequireAnyUser")]
        public async Task<IActionResult> GetAllProfiles()
        {
            var query = new GetAllUserProfile();
            var response = await _mediator.Send(query);
            var profiles = _mapper.Map<List<UserProfileResponse>>(response.PayLoad);
            return Ok(profiles);
        }


        [HttpGet]
        [Route(ApiRoutes.UserProfile.IdRoute)]
        [Authorize(Policy = "RequireAnyUser")]
        public async Task<IActionResult> GetUserProfileById(Guid userProfileId)
        {
            var query = new GetUserProfileById { UserProfileId = userProfileId };
            var result = await _mediator.Send(query);
            var mapped = _mapper.Map<UserProfileResponse>(result.PayLoad);

            if (result.IsError) return HandleErrorResponse(result.Errors);
            
            return Ok(mapped);
        }

        [HttpPatch]
        [Route(ApiRoutes.UserProfile.IdRoute)]
        [ValidateModel]
        [Authorize(Policy = "RequireAdminUser")]
        public async Task<IActionResult> UpdateUserProfile(Guid userProfileId, UserProfileUpdateRequest updatedProfile)
        {
            var command = _mapper.Map<UpdateUserProfileBasicInfo>(updatedProfile);
            command.Id = userProfileId;
            var response = await _mediator.Send(command);

            return response.IsError ? HandleErrorResponse(response.Errors) : NoContent();
        }

        [HttpDelete]
        [Route(ApiRoutes.UserProfile.IdRoute)]
        [Authorize(Policy = "RequireAdminUser")]
        public async Task<IActionResult> DeleteUserProfile(Guid userProfileId)
        {
            var command = new DeleteUserProfileCommand() { UserProfileId = userProfileId };
            var response = await _mediator.Send(command);

            return response.IsError ? HandleErrorResponse(response.Errors) : NoContent();
        }
    }
}
