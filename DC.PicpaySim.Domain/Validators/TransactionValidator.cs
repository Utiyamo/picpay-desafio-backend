using DC.PicpaySim.Domain.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.PicpaySim.Domain.Validators
{
    internal class TransactionValidator
    {
    }

    public class CreateTransactionValidator : AbstractValidator<CreateTransactionCommand>
    {
        public CreateTransactionValidator()
        {
            RuleFor(command => command.PayerID).NotEmpty().WithMessage("PayerID is required");
            RuleFor(command => command.PayeeID).NotEmpty().WithMessage("PayeeID is required");
            RuleFor(command => command.Value).NotEmpty().WithMessage("Value is required");
        }
    }
}
