using App.Desafio.Blog.Domain.Dtos.Requests;
using App.Desafio.Blog.Domain.Dtos.Responses;
using App.Desafio.Blog.Domain.Entities;

namespace App.Desafio.Blog.Domain.Interfaces
{
    public interface IUserService
    {
        Task<UserRegisterResponse> CreateUserAsync(UserRegisterRequest request);
        Task<User> AuthenticateAsync(string email, string password);
        Task<UserRegisterResponse> GetUserByEmailAsync(string email);
    }

}
