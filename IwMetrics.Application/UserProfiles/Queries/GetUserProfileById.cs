using IwMetrics.Application.Models;
using IwMetrics.Domain.Aggregates.UserProfileAggregate;
using MediatR;

namespace IwMetrics.Application.UserProfiles.Queries
{
    public class GetUserProfileById : IRequest<OperationResult<UserProfile>>
    {
        public Guid UserProfileId { get; set; }
    }
}
