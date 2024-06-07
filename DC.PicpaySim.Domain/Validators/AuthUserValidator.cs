using DC.PicpaySim.Domain.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.PicpaySim.Domain.Validators
{
    public class AuthUserValidator : AbstractValidator<AuthUserCommand>
    {
        public AuthUserValidator()
        {
            RuleFor(command => command.Email).NotEmpty().EmailAddress().WithMessage("Email is required");
            RuleFor(command => command.Password).NotEmpty().WithMessage("Password is required");
        }
    }
}
