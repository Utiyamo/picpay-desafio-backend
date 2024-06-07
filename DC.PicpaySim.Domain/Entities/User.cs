using DC.PicpaySim.Domain.Commons;
using DC.PicpaySim.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.PicpaySim.Domain.Entities
{
    public class User : BaseEntity<long>
    {
        public string NomeCompleto { get; set; }
        public string Documento { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public ETypeUser TypeUser { get; set; }

        public User() { }

        public User(string name, string document, string email, string password, ETypeUser typeUser)
        {
            this.ExternalId = new Guid();
            this.NomeCompleto = name;
            this.Documento = document;
            this.Email = email;
            this.Senha = EncryptHelper.sha256(password);
            this.CreatedAte = DateTime.Now;
            this.TypeUser = typeUser;
        }

        public User(long id, Guid externalID, string name, string document, string email, string password, ETypeUser typeUser, DateTime createdAte)
        {
            this.Id = id;
            this.ExternalId = externalID;
            this.NomeCompleto = name;
            this.Documento = document;
            this.Email = email;
            this.Senha = password;
            this.TypeUser = typeUser;
            this.CreatedAte = createdAte;
        }
    }
}
