using IwMetrics.Application.Models;
using IwMetrics.Application.UserProfiles.Command;
using IwMetrics.DAL;
using IwMetrics.Domain.Aggregates.UserProfileAggregate;
using IwMetrics.Domain.Validators.UserProfileValidator;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using IwMetrics.Application.Models;
using IwMetrics.Application.Enums;

namespace IwMetrics.Application.UserProfiles.CommandHandler
{
    internal class UpdateUserProfileBasicInfoHandler : IRequestHandler<UpdateUserProfileBasicInfo, OperationResult<UserProfile>>
    {
        private readonly DataContext _ctx;

        public UpdateUserProfileBasicInfoHandler(DataContext ctx)
        {
            _ctx = ctx;    
        }

        public async Task<OperationResult<UserProfile>> Handle(UpdateUserProfileBasicInfo request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<UserProfile>();

            try
            {
                var userProfile = await _ctx.UserProfiles.FirstOrDefaultAsync(up => up.UserProfileId == request.UserProfileId);

                if (userProfile == null)
                {
                    result.IsError = true;
                    var error = new Error { Code = ErrorCode.NotFound, Message = $"No User Profile with Id {request.UserProfileId} found" };
                    result.Errors.Add(error);
                    return result;
                }

                userProfile.UpdateBasicInfo(userProfile.BasicInfo.WithUpdatedFields(request.FirstName, request.LastName, 
                    request.EmailAddress, request.Phone, request.CurrentCity));


                _ctx.UserProfiles.Update(userProfile);
                await _ctx.SaveChangesAsync();

                result.PayLoad = userProfile;
                return result;
            }
            catch (Exception e)
            {
                var error = new Error { Code = ErrorCode.ServerError, Message = e.Message };
                result.IsError = true;
                result.Errors.Add(error);
                return result;
            }
            

            return result;
        }
    }
}
