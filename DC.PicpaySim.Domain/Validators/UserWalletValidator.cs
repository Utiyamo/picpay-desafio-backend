using DC.PicpaySim.Domain.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.PicpaySim.Domain.Validators
{
    internal class UserWalletValidator
    {
    }

    public class CreateWalletCommandValidator : AbstractValidator<CreateWalletCommand>
    {
        public CreateWalletCommandValidator()
        {
            RuleFor(command => command.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(command => command.CreditValue)
                .NotNull().WithMessage("CreditValue is required")
                .GreaterThanOrEqualTo(0).WithMessage("CreditValue must be greater than or equal to 0")
                .ScalePrecision(2, 18).WithMessage("CreditValue must not have more than 2 decimal places");
        }
    }
}
