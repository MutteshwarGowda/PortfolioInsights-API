﻿using IwMetrics.Application.Identity.Dtos;
using IwMetrics.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IwMetrics.Application.Identity.Commands
{
    public class LoginCommand : IRequest<OperationResult<IdentityUserProfileDto>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
