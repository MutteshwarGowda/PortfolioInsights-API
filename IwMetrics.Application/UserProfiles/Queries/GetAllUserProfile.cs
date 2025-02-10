using IwMetrics.Application.Models;
using IwMetrics.Domain.Aggregates.UserProfileAggregate;
using MediatR;

namespace IwMetrics.Application.UserProfiles.Queries
{
    public class GetAllUserProfile : IRequest<OperationResult<IEnumerable<UserProfile>>>
    {
    }
}
