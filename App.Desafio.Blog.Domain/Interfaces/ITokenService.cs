using App.Desafio.Blog.Domain.Dtos.Responses;
using App.Desafio.Blog.Domain.Entities;

namespace App.Desafio.Blog.Domain.Interfaces
{
    public interface ITokenService
    {
        Task<TokenResponse> GenerateJwtTokenAsync(User user);
    }
}
