using IwMetrics.Domain.Exceptions;
using IwMetrics.Domain.Validators.PortfolioAssestsValidator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IwMetrics.Domain.Aggregates.PortfolioAssets
{
    public class Asset
    {
        public Guid AssetId { get; private set; }
        public string Name { get; private set; }
        public decimal Value { get; private set; }
        public AssetType Type { get; private set; }
        public Guid PortfolioId { get; private set; }
        public Portfolio Portfolio { get; private set; } // Navigation property to Portfolio
       
        public DateTime CreatedAt { get; private set; }
        public DateTime? DateModified { get; private set; }

        private Asset()
        {
                
        }

        public static Asset CreateAsset(string name, decimal value, AssetType type, Guid portfolioId)
        {
            var validator = new AssetValidator();

            var objectToValidate = new Asset
            {
                AssetId = Guid.NewGuid(),
                Name = name,
                Value = value,
                Type = type,
                PortfolioId = portfolioId,
                CreatedAt = DateTime.UtcNow
            };

            var validationResult = validator.Validate(objectToValidate);

            if (validationResult.IsValid) return objectToValidate;

            var exception = new AssetNotValidException("The Asset is Not Valid");

            foreach (var error in validationResult.Errors)
            {
                exception.ValidationErrors.Add(error.ErrorMessage);
            }

            throw exception;
        }

        public void UpdateAsset(string? newName, decimal? newValue, AssetType? newType)
        {
            Name = !string.IsNullOrWhiteSpace(newName) ? newName : this.Name;
            Value = newValue.HasValue ? newValue.Value : this.Value;
            Type = newType.HasValue ? newType.Value : this.Type;
            DateModified = DateTime.UtcNow;
        }
    }
}
