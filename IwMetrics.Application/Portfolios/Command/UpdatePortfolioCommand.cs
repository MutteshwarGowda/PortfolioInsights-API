using IwMetrics.Application.Models;
using IwMetrics.Domain.Aggregates.PortfolioAssets;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IwMetrics.Application.Portfolios.Command
{
    public class UpdatePortfolioCommand : IRequest<OperationResult<Portfolio>>
    {
        public Guid PortfolioId { get; set; } 
        public string? Name { get; set; } 
        public RiskLevel? RiskLevel { get; set; } 
        public Guid ManagerId { get; set; }
       
    }
}
