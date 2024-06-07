using DC.PicpaySim.Domain.Commands;
using DC.PicpaySim.Domain.Commons;
using DC.PicpaySim.Domain.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.PicpaySim.Domain.Validators
{
    internal class UserValidator
    {
    }

    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(command => command.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(command => command.Password).NotEmpty().WithMessage("Password is required");
            RuleFor(command => command.Email).NotEmpty().EmailAddress().WithMessage("Email is required");
            RuleFor(command => command.TypeUser).NotEmpty().WithMessage("User type is required");

            RuleFor(command => command.Document)
                .Must(BeValidDocument).WithMessage("Invalid document number.");
        }

        private bool BeValidDocument(string document)
        {
            bool isValid = true;
            if (String.IsNullOrEmpty(document))
                isValid = false;
            else if (String.IsNullOrWhiteSpace(document))
                isValid = false;

            if (document.Length == 11)
                isValid = DocumentHelpers.IsValidCPF(document);
            else if (document.Length == 14)
                isValid = DocumentHelpers.IsValidCNPJ(document);
            else
                isValid = false;

            return isValid;
        }
    }

    public class GetUserQueryValidator : AbstractValidator<GetUserQuery>
    {
        public GetUserQueryValidator()
        {
            RuleFor(query => query.Id).NotEmpty().WithMessage("ID is required");
        }
    }
}
