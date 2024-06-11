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
    internal class UserWalletCommands
    {

    }

    public class CreateWalletCommand : IRequest<BaseResponse<UserWalletWithUserDTO>>
    {
        public string Name { get; set; }
        public Decimal CreditValue { get; set; }
        public bool IsPrimary { get; set; }
        public Guid UserID { get; set; }

        public CreateWalletCommand() { }

        public CreateWalletCommand(string name, decimal creditValue, bool isPrimary, Guid userID)
        {
            this.Name = name;
            this.CreditValue = creditValue;
            this.IsPrimary = isPrimary;
            this.UserID = userID;
        }
    }

    public class GetWalletQuery : IRequest<BaseResponse<UserWalletDTO>>
    {
        public Guid Id { get; set; }

        public GetWalletQuery() { }

        public GetWalletQuery(Guid id)
        {
            Id = id;
        }
    }

    public class GetWalletByUserQuery : IRequest<BaseResponse<UserWithWalletsDTO>>
    {
        public Guid UserID { get; set; }

        public GetWalletByUserQuery() { }
        public GetWalletByUserQuery(Guid userID)
        {
            this.UserID = userID;
        }
    }
}
