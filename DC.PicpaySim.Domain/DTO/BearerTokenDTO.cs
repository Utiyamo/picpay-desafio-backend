using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.PicpaySim.Domain.DTO
{
    public class BearerTokenDTO
    {
        public string BearerToken { get; set; }
        public DateTime Expiration { get; set; }
    }
}
