using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.PicpaySim.Domain.Entities
{
    public class Transactions : BaseEntity<long>
    {
        public long WalletFrom { get; set; }
        public long WalletTo { get; set; }
        public DateTime TransactionDate { get; set; }
        public Decimal Amount { get; set; }

        public Transactions() { }

        public Transactions(long walletFrom, long walletTo, DateTime transactionDate, decimal amount)
        {
            this.ExternalId = Guid.NewGuid();
            this.CreatedAte = new DateTime();
            WalletFrom = walletFrom;
            WalletTo = walletTo;
            TransactionDate = transactionDate;
            Amount = amount;
        }
    }
}
