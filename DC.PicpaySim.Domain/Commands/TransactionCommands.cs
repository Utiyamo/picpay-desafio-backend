using DC.PicpaySim.Domain.Commons;
using DC.PicpaySim.Domain.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.PicpaySim.Domain.Commands
{
    internal class TransactionCommands
    {
    }

    public class CreateTransactionCommand : IRequest<BaseResponse<TransactionDTO>>
    {
        public Guid PayerID { get; set; }
        public Guid PayeeID { get; set; }
        public Decimal Value { get; set; }

        public CreateTransactionCommand() { }

        public CreateTransactionCommand(Guid payer, Guid payee, decimal value)
        {
            this.PayerID = payer;
            this.PayeeID = payee;
            this.Value = value;
        }
    }
}
