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
    internal class UserCommands
    {
    }

    public class CreateUserCommand : IRequest<BaseResponse<UserDTO>>
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Document { get; set; }
        public ETypeUser TypeUser { get; set; }

        public CreateUserCommand(string name, string password, string email, string document, ETypeUser typeUser)
        {
            Name = name;
            Password = password;
            Email = email;
            Document = document;
            TypeUser = typeUser;
        }
    }

    public class GetUserQuery : IRequest<BaseResponse<UserDTO>>
    {
        public Guid Id { get; set; }

        public GetUserQuery(Guid id)
        {
            this.Id = id;
        }
    }

    public class GetUserByDocumentQuery : IRequest<BaseResponse<UserDTO>>
    {
        public string Document { get; set; }

        public GetUserByDocumentQuery(string document)
        {
            this.Document = document;
        }
    }
}
