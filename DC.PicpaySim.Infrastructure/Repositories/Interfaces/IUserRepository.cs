using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.PicpaySim.Infrastructure.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<DC.PicpaySim.Domain.Entities.User> FindById(long id);
    }
}
