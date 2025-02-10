using IwMetrics.Domain.Exceptions;
using IwMetrics.Domain.Validators.UserProfileValidator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IwMetrics.Domain.Aggregates.UserProfileAggregate
{
    public class BasicInfo
    {
        private BasicInfo()
        {

        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string EmailAddress { get; private set; }
        public string Phone { get; private set; }
        public string CurrentCity { get; private set; }

        public static BasicInfo CreateBasicInfo(string firstName, string lastName, string emailAddress, string phone, string currentCity)
        {
            var validator = new BasicInfoValidator();


            //TO DO Implement Validation, Error handling
            var objToValidate = new BasicInfo()
            {
                FirstName = firstName,
                LastName = lastName,
                EmailAddress = emailAddress,
                Phone = phone,
                CurrentCity = currentCity

            };

            var validationResult = validator.Validate(objToValidate);
            
            if (validationResult.IsValid) return objToValidate;

            var exception = new UserProfileNotValidException("The User Profile is Not Valid");

            foreach (var error in validationResult.Errors)
            {
                exception.ValidationErrors.Add(error.ErrorMessage);
            }

            throw exception;

        }

        public BasicInfo WithUpdatedFields(string? firstName, string? lastName, string? emailAddress, string? phone, string? city)
        {
            return new BasicInfo()
            {
                FirstName = !string.IsNullOrWhiteSpace(firstName) ? firstName : this.FirstName,
                LastName = !string.IsNullOrWhiteSpace(lastName) ? lastName : this.LastName,
                EmailAddress = !string.IsNullOrWhiteSpace(emailAddress) ? emailAddress : this.EmailAddress,
                Phone = !string.IsNullOrWhiteSpace(phone) ? phone : this.Phone,
                CurrentCity= !string.IsNullOrWhiteSpace(city) ? city : this.CurrentCity
            };
        }
    }
}
