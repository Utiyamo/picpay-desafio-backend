using DC.PicpaySim.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.PicpaySim.Domain.DTO
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string NomeCompleto { get; set; }
        public string Documento { get; set; }
        public string Email { get; set; }
        public ETypeUser TypeUser { get; set; }

        public UserDTO() { }

        public UserDTO(Guid id, string nomeCompleto, string documento, string email, ETypeUser typeUser)
        {
            Id = id;
            NomeCompleto = nomeCompleto;
            Documento = documento;
            Email = email;
            TypeUser = typeUser;
        }
    }
}
