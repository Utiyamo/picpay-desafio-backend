using DC.PicpaySim.Domain.Commons;
using DC.PicpaySim.Domain.DTO;
using DC.PicpaySim.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.PicpaySim.Domain.Commands
{
    internal class AuthCommands
    {
    }

    public class AuthUserCommand : IRequest<BaseResponse<UserAuthDTO>>
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public AuthUserCommand(string email, string password)
        {
            Password = password;
            Email = email;
        }
    }
}
