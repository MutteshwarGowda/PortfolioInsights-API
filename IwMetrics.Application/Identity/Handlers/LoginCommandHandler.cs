using AutoMapper;
using IwMetrics.Application.Enums;
using IwMetrics.Application.Identity.Commands;
using IwMetrics.Application.Identity.Dtos;
using IwMetrics.Application.Models;
using IwMetrics.Application.Options;
using IwMetrics.Application.Services;
using IwMetrics.DAL;
using IwMetrics.Domain.Aggregates.UserProfileAggregate;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IwMetrics.Application.Identity.Handlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, OperationResult<IdentityUserProfileDto>>
    {
        private readonly DataContext _ctx;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IdentityService _identityService;
        private readonly IMapper _mapper;
        private OperationResult<IdentityUserProfileDto> _result = new();

        public LoginCommandHandler(DataContext ctx, UserManager<IdentityUser> userManager, IdentityService identityService, IMapper mapper)
        {
            _ctx = ctx;     
            _userManager = userManager;
            _identityService = identityService;
            _mapper = mapper;
        }

        public async Task<OperationResult<IdentityUserProfileDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            
            try
            {
                var identityUser = await ValidateAndGetIdentityAsync(request);

                if (_result.IsError) return _result;

                var userProfile = await _ctx.UserProfiles.FirstOrDefaultAsync(up => up.IdentityId == identityUser.Id);

                _result.PayLoad = _mapper.Map<IdentityUserProfileDto>(userProfile);
                _result.PayLoad.UserName = identityUser.UserName;
                _result.PayLoad.Token = GetJwtString(identityUser, userProfile);
                return _result;
            }
            catch (Exception e)
            {

                _result.AddUnknownError(e.Message);
            }

            return _result;
        }

        private async Task<IdentityUser> ValidateAndGetIdentityAsync(LoginCommand request)
        {
            var identityUser = await _userManager.FindByEmailAsync(request.UserName);

            if (identityUser is null)
                _result.AddError(ErrorCode.IdentityUserDoesNotExist, IdentityErrorMessages.IdentityUserDoesNotExist);

            var validPassword = await _userManager.CheckPasswordAsync(identityUser, request.Password);

            if (!validPassword)
                _result.AddError(ErrorCode.IncorrectPassword, IdentityErrorMessages.IncorrectPassword);

            return identityUser;
        }

        private string GetJwtString(IdentityUser identity, UserProfile profile)
        {
            var claimsIdentity = new ClaimsIdentity(new Claim[]
            {
                    new Claim(JwtRegisteredClaimNames.Sub, identity.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, identity.Email),
                    new Claim("IdentityId", identity.Id),
                    new Claim("UserProfileId", profile.UserProfileId.ToString())
            });

            var token = _identityService.CreateSecurityToken(claimsIdentity);
            return _identityService.WriteToken(token);
        }
    }
}
