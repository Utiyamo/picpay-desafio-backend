using DC.PicpaySim.Domain.Entities;
using DC.PicpaySim.Infrastructure.ORM;
using DC.PicpaySim.Infrastructure.Repositories.Abstractions;
using DC.PicpaySim.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.PicpaySim.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User, long>
    {
        public UserRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }
    }
}
