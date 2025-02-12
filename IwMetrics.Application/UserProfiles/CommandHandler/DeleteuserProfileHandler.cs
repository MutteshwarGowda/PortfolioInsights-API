using IwMetrics.Application.Enums;
using IwMetrics.Application.Models;
using IwMetrics.Application.UserProfiles.Command;
using IwMetrics.Infrastructure;
using IwMetrics.Domain.Aggregates.UserProfileAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IwMetrics.Application.UserProfiles.CommandHandler
{
    internal class DeleteuserProfileHandler : IRequestHandler<DeleteUserProfileCommand, OperationResult<UserProfile>>
    {
        private readonly DataContext _ctx;

        public DeleteuserProfileHandler(DataContext ctx)
        {
            _ctx = ctx;     
        }
        public async Task<OperationResult<UserProfile>> Handle(DeleteUserProfileCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<UserProfile>();

            var userProfile = await _ctx.UserProfiles.FirstOrDefaultAsync(up => up.UserProfileId == request.UserProfileId, cancellationToken);

            if (userProfile is null)
            {
                result.IsError = true;
                var error = new Error { Code = ErrorCode.NotFound, Message = $"No User Profile with Id {request.UserProfileId} found" };
                result.Errors.Add(error);
                return result;
            }

            _ctx.UserProfiles.Remove(userProfile);
            await _ctx.SaveChangesAsync(cancellationToken);

            result.PayLoad = userProfile;
            return result;
        }
    }
}
