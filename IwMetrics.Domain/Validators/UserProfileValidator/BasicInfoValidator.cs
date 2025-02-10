using FluentValidation;
using IwMetrics.Domain.Aggregates.UserProfileAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IwMetrics.Domain.Validators.UserProfileValidator
{
    public class BasicInfoValidator : AbstractValidator<BasicInfo>
    {
        public BasicInfoValidator()
        {
            RuleFor(info => info.FirstName).NotNull().WithMessage("First Name is Required, It is Currently Null")
                                           .MinimumLength(3).WithMessage("First Name must be atleast 3 characters long")
                                           .MaximumLength(50).WithMessage("First Name should not exceed 50 charcaters");

            RuleFor(info => info.LastName).NotNull().WithMessage("Last Name is Required, It is Currently Null")
                                          .MinimumLength(3).WithMessage("Last Name must be atleast 3 characters long")
                                          .MaximumLength(50).WithMessage("Last Name should not exceed 50 charcaters");

            RuleFor(info => info.EmailAddress).NotNull().WithMessage("Email address is Required")
                                              .EmailAddress().WithMessage("Provided Email address is not valid");
        }
    }
}
