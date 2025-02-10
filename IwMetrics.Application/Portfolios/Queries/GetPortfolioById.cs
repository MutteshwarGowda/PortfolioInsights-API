﻿using IwMetrics.Application.Models;
using IwMetrics.Domain.Aggregates.PortfolioAssets;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IwMetrics.Application.Portfolios.Queries
{
    public class GetPortfolioById : IRequest<OperationResult<Portfolio>>
    {
        public Guid PortfolioId { get; set; }
    }
}
