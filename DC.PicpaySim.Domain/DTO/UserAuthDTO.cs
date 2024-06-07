using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.PicpaySim.Domain.DTO
{
    public class UserAuthDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string BearerToken { get; set; }
        public DateTime Expiration { get; set; }

        public UserAuthDTO() { }

        public UserAuthDTO(Guid id, string name, string email, string bearerToken, DateTime expiration)
        {
            this.Id = id;
            this.Name = name;
            this.Email = email;
            this.BearerToken = bearerToken;
            this.Expiration = expiration;
        }

        public UserAuthDTO(Guid id, string name, string email)
        {
            this.Id = id;
            this.Name = name;
            this.Email = email;
            this.BearerToken = String.Empty;
            this.Expiration = new DateTime();
        }
    }
}
