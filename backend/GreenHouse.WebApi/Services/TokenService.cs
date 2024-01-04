using GreenHouse.Domain.Entities;
using GreenHouse.Domain.Interfaces;
using GreenHouse.WebApi.Configurations;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace GreenHouse.WebApi.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtConfig _jwtConfig;
        public TokenService(JwtConfig jwtConfig)
        {
            _jwtConfig = jwtConfig ?? throw new ArgumentNullException(nameof(jwtConfig));
        }
        public virtual string GenerateToken(Admin admin)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, admin.Id.ToString()),
                }),
                Expires = DateTime.UtcNow.Add(_jwtConfig.LifeTime),
                Audience = _jwtConfig.Audience,
                Issuer = _jwtConfig.Issuer,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(_jwtConfig.SigningKeyBytes),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
