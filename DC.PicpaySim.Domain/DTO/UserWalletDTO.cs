using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.PicpaySim.Domain.DTO
{
    public class UserWalletDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Primary { get; set; }
        public decimal CreditAmmout { get; set; }

        public UserWalletDTO() { }

        public UserWalletDTO(Guid id, string name, bool primary, decimal creditAmmout)
        {
            Id = id;
            Name = name;
            Primary = primary;
            CreditAmmout = creditAmmout;
        }
    }

    public class UserWalletWithUserDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Primary { get; set; }
        public decimal CreditAmmout { get; set; }
        public UserDTO User { get; set; }

        public UserWalletWithUserDTO() { }

        public UserWalletWithUserDTO(Guid id, string name, bool primary, decimal creditAmmout, UserDTO user)
        {
            Id = id;
            Name = name;
            Primary = primary;
            CreditAmmout = creditAmmout;
            User = user;
        }
    }
}
