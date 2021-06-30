using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Bilbayt.Web.API.utils
{
    public class JWTokenCreator
    {
        public static string GenerateAccessToken(IEnumerable<Claim> claims, string jwtSigningKey, double expiry, string issuer)
        {
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwtSigningKey));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddSeconds(expiry),
                SigningCredentials = credentials,
                Issuer = issuer,
                Audience = issuer
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
