using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.PicpaySim.Domain.Entities
{
    public class UserWallet : BaseEntity<long>
    {
        public string Name { get; set; }
        public bool Active { get; set; }
        public bool Primary { get; set; }
        public decimal CreditAmmout { get; set; }

        [ForeignKey("user")]
        public long UserID { get; set; }
        public virtual User User { get; set; }

        public UserWallet() { }

        public UserWallet(string name, long userID, bool active = true, bool isPrimary = true, decimal credit = 0)
        {
            this.ExternalId = Guid.NewGuid();
            this.Name = name;
            this.UserID = userID;
            this.Active = active;
            this.Primary = isPrimary;
            this.CreditAmmout = credit;
            this.CreatedAte = DateTime.Now;
        }

        public UserWallet(long id, Guid externalID, string name, bool active, bool primary, decimal credit, long userID, DateTime created)
        {
            this.Id = id;
            this.ExternalId = externalID;
            this.Name = name;
            this.Active = active;
            this.Primary = primary;
            this.CreatedAte = created;
            this.CreditAmmout = credit;
            this.UserID = userID;
        }
    }
}
