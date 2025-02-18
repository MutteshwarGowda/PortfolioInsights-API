
namespace IwMetrics.Application.Identity.QueryHandler
{
    public class GetAllUsersHandler : IRequestHandler<GetAllUsers, OperationResult<List<IdentityUserDto>>>
    {
        private readonly DataContext _ctx;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;

        public GetAllUsersHandler(DataContext ctx, UserManager<IdentityUser> userManager, IMapper mapper)
        {
            _ctx = ctx;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<OperationResult<List<IdentityUserDto>>> Handle(GetAllUsers request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<List<IdentityUserDto>>();

            try
            {
                var users = await _userManager.Users.ToListAsync(cancellationToken);
                result.PayLoad = _mapper.Map<List<IdentityUserDto>>(users);
                
            }
            catch (Exception e)
            {

                result.AddUnknownError(e.Message);
            }

            return result;
        }
    }
}
