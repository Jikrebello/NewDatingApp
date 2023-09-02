using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace API.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration configuration)
        {
            byte[] key = SHA512.HashData(Encoding.UTF8.GetBytes(configuration["TokenKey"]));
            _key = new SymmetricSecurityKey(key);
        }

        public string CreateToken(User user)
        {
            var claims = new List<Claim> { new(JwtRegisteredClaimNames.Email, user.EmailAddress) };

            var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = credentials,
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
