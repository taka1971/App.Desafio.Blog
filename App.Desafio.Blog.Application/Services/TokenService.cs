using App.Desafio.Blog.Domain.Dtos.Responses;
using App.Desafio.Blog.Domain.Entities;
using App.Desafio.Blog.Domain.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace App.Desafio.Blog.Application.Services
{
    public class TokenService : ITokenService
    {        
        private readonly AppSettings _settings;

        public TokenService(IOptions<AppSettings> appSettings) 
        {
            _settings = appSettings.Value;     
        }
        public async Task<TokenResponse> GenerateJwtTokenAsync(User user)
        {
            return await Task.Run(() =>
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_settings.JwtSettings.Secret);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Email, user.Email)

                    }),
                    Audience = _settings.JwtSettings.Audience,
                    Expires = DateTime.UtcNow.AddMinutes(_settings.JwtSettings.ExpiresInMinutes),
                    Issuer = _settings.JwtSettings.Issuer,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);

                return new TokenResponse(tokenHandler.WriteToken(token));
            });
        }
    }
}
