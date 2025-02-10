using IwMetrics.Application.Models;
using IwMetrics.Domain.Aggregates.PortfolioAssets;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IwMetrics.Application.Assets.Command
{
    public class CreateAssetCommand : IRequest<OperationResult<Asset>>
    {
        public string Name { get; set; }
        public decimal Value { get; set; }
        public AssetType Type { get; set; }
        public Guid PortfolioId { get; set; }
    }
}
