using FluentValidation;
using IwMetrics.Domain.Aggregates.PortfolioAssets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IwMetrics.Domain.Validators.PortfolioAssestsValidator
{
    public class AssetValidator : AbstractValidator<Asset>
    {
        public AssetValidator()
        {
            RuleFor(asset => asset.AssetId)
            .NotEmpty().WithMessage("Asset ID cannot be empty.");

            RuleFor(asset => asset.Name)
                .NotEmpty().WithMessage("Asset name is required.")
                .MinimumLength(3).WithMessage("Asset name must be at least 3 characters long.")
                .MaximumLength(100).WithMessage("Asset name must not exceed 100 characters.");

            RuleFor(asset => asset.Value)
                .GreaterThan(0).WithMessage("Asset value must be greater than zero.");

            RuleFor(asset => asset.Type)
                .NotNull().WithMessage("Asset type is required.");

            RuleFor(asset => asset.PortfolioId)
                .NotEmpty().WithMessage("Portfolio ID cannot be empty."); // Ensures Asset belongs to a Portfolio

        }
    }
}
