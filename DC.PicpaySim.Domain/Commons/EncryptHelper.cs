using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DC.PicpaySim.Domain.Commons
{
    public static class EncryptHelper
    {
        public static String sha256(string valueBase)
        {
            var inputBytes = Encoding.UTF8.GetBytes(valueBase);
            var inputHash = SHA256.HashData(inputBytes);
            return Convert.ToHexString(inputHash);
        }
    }
}
