using DC.PicpaySim.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.PicpaySim.Domain.Interfaces
{
    public interface IAuthenticationService
    {
        BearerTokenDTO GenerateToken(UserDTO user);
    }
}
