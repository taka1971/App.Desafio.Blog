using App.Desafio.Blog.Domain.Dtos.Responses;
using App.Desafio.Blog.Domain.Entities;

namespace App.Desafio.Blog.Domain.Extensions
{
    public static class UserExtensions
    {
        public static UserRegisterResponse ToDto(this User user)
        {
            return new UserRegisterResponse(user.Id, user.Username, user.Email);
        }


    }
}
