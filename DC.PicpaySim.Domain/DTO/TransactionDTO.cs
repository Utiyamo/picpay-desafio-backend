using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.PicpaySim.Domain.DTO
{
    public class TransactionDTO
    {
        public Guid Id { get; set; }
        public Guid PayerID { get; set; }
        public Guid PayeeID { get; set; }
        public Decimal Value { get; set; }
        public DateTime TransactionDate { get; set; }

        public TransactionDTO () { }

        public TransactionDTO(Guid id, Guid payerID, Guid payeeID, decimal value, DateTime transactionDate)
        {
            Id = id;
            PayerID = payerID;
            PayeeID = payeeID;
            Value = value;
            TransactionDate = transactionDate;
        }
    }
}
