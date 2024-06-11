using DC.PicpaySim.Domain.Entities;
using DC.PicpaySim.Infrastructure.ORM;
using DC.PicpaySim.Infrastructure.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.PicpaySim.Infrastructure.Repositories
{
    public class UserWalletRepository : BaseRepository<UserWallet, long>
    {
        public UserWalletRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }
    }
}
