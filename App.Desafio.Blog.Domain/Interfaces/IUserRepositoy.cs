using App.Desafio.Blog.Domain.Entities;

namespace App.Desafio.Blog.Domain.Interfaces
{
    public interface IUserRepository
    {        
        Task<User> CreateUserAsync(User user);
        Task<User> GetUserByEmailAsync(string email);        
    }
}
