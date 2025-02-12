using IwMetrics.Application.Models;
using IwMetrics.Application.UserProfiles.Queries;
using IwMetrics.Infrastructure;
using IwMetrics.Domain.Aggregates.UserProfileAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace IwMetrics.Application.UserProfiles.QueryHandler
{
    internal class GetAllUserProfileQueryHandler : IRequestHandler<GetAllUserProfile, OperationResult<IEnumerable<UserProfile>>>
    {
        private readonly DataContext _ctx;
        public GetAllUserProfileQueryHandler(DataContext ctx)
        {
            _ctx = ctx;    
        }

        public async Task<OperationResult<IEnumerable<UserProfile>>> Handle(GetAllUserProfile request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<IEnumerable<UserProfile>>();

            result.PayLoad = await _ctx.UserProfiles.ToListAsync();

            return result;
        }
    }
}
