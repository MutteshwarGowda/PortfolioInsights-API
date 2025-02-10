using FluentValidation;
using IwMetrics.Domain.Aggregates.PortfolioAssets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IwMetrics.Domain.Validators.PortfolioAssestsValidator
{
    public class PortfolioValidator : AbstractValidator<Portfolio>
    {
        public PortfolioValidator()
        {
            RuleFor(portfolio => portfolio.PortfolioId)
            .NotEmpty().WithMessage("Portfolio ID cannot be empty.");

            RuleFor(portfolio => portfolio.Name)
                .NotEmpty().WithMessage("Portfolio name is required.")
                .MinimumLength(3).WithMessage("Portfolio name must be at least 3 characters long.")
                .MaximumLength(100).WithMessage("Portfolio name must not exceed 100 characters.");

            RuleFor(portfolio => portfolio.UserProfileId)
                .NotEmpty().WithMessage("Manager ID cannot be empty."); // Ensures the Portfolio is assigned to a Manager

            RuleFor(portfolio => portfolio.RiskLevel)
                .NotNull().WithMessage("Risk Level is required.");

        } 
    }
}
