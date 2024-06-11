using DC.PicpaySim.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.PicpaySim.Domain.DTO
{
    public class UserWithWalletsDTO
    {
        public Guid Id { get; set; }
        public string NomeCompleto { get; set; }
        public string Documento { get; set; }
        public string Email { get; set; }
        public ETypeUser TypeUser { get; set; }
        public IEnumerable<UserWalletDTO> Wallets { get; set; }

        public UserWithWalletsDTO() { }

        public UserWithWalletsDTO(Guid id, string nomeCompleto, string documento, string email, ETypeUser typeUser, IEnumerable<UserWalletDTO> wallets)
        {
            Id = id;
            NomeCompleto = nomeCompleto;
            Documento = documento;
            Email = email;
            TypeUser = typeUser;
            Wallets = wallets;
        }
    }
}
