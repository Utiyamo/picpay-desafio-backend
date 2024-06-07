using DC.PicpaySim.Domain.DTO;
using DC.PicpaySim.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DC.PicpaySim.Infrastructure.Middleware
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly String _jwtKey;

        public AuthenticationService(IConfiguration configuration)
        {
            _jwtKey = configuration["JWT:token"];
        }

        public BearerTokenDTO GenerateToken(UserDTO user)
        {
            var claims = new[]{
                            new Claim(JwtRegisteredClaimNames.UniqueName, user.NomeCompleto),
                            new Claim(JwtRegisteredClaimNames.Email, user.Email),
                            new Claim(JwtRegisteredClaimNames.Jti, user.Id.ToString())
                        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            DateTime expiration = DateTime.UtcNow.AddHours(1);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: "PicpaySIM",
                audience: "Picpay",
                claims: claims,
                expires: expiration,
                signingCredentials: creds
            );

            return new BearerTokenDTO()
            {
                BearerToken = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }
}
