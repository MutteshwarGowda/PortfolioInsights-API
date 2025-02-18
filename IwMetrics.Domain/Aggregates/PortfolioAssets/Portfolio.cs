using IwMetrics.Domain.Aggregates.UserProfileAggregate;
using IwMetrics.Domain.Exceptions;
using IwMetrics.Domain.Validators.PortfolioAssestsValidator;
using System;
using System.Xml.Linq;

namespace IwMetrics.Domain.Aggregates.PortfolioAssets
{
    public class Portfolio
    {
        private Portfolio()
        {

        }

        public Guid PortfolioId { get; private set; } 
        public string Name { get; private set; } 
        public decimal TotalValue { get; private set; } 
        public RiskLevel RiskLevel { get; private set; } 
        public UserProfile UserProfile { get; private set; }
        public Guid UserProfileId { get; private set; }
        public DateTime CreatedAt { get; private set; } 
        public DateTime? DateModified { get; private set; }

        private readonly List<Asset> _assets = new List<Asset>();
        public IEnumerable<Asset> Assets => _assets.AsReadOnly();


        // Factory Method
        public static Portfolio CreateNew(string name, UserProfile userProfile, RiskLevel riskLevel)
        {
            var validator = new PortfolioValidator();

            var objectToValidate = new Portfolio 
            {
                PortfolioId = Guid.NewGuid(), 
                Name = name,
                UserProfile = userProfile,
                UserProfileId = userProfile.UserProfileId,
                RiskLevel = riskLevel, 
                CreatedAt = DateTime.UtcNow 
            };

            var validationResult = validator.Validate(objectToValidate);

            if (validationResult.IsValid) return objectToValidate;

            var exception = new PortfolioNotValidException("The Portfolio is Not Valid");

            foreach (var error in validationResult.Errors)
            {
                exception.ValidationErrors.Add(error.ErrorMessage);
            }

            throw exception;
        }

        public void UpdatePortfolio(string? name, RiskLevel? riskLevel, UserProfile userProfile)
        {
            Name = !string.IsNullOrWhiteSpace(name) ? name : this.Name;
            RiskLevel = riskLevel.HasValue ? riskLevel.Value : this.RiskLevel;
            UserProfileId = userProfile.UserProfileId;
            DateModified = DateTime.UtcNow;
        }

        public void AddAsset(Asset asset)
        {
            _assets.Add(asset);
            TotalValue += asset.Value;
        }

        public void UpdateAssetValue(decimal oldValue, decimal newValue)
        {
            TotalValue += (newValue - oldValue);
        }

        public void RemoveAsset(Asset asset)
        {
            _assets.Remove(asset);
            TotalValue -= asset.Value;
        }
    }
}
