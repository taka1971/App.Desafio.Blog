using App.Desafio.Blog.Domain.Dtos.Requests;
using App.Desafio.Blog.Domain.Entities;

namespace App.Desafio.Blog.Domain.Extensions
{
    public static class DtoExtensions
    {
        public static Post DtoToPost(this CreatePostRequest request, Guid userId)
        {
            return new Post { Title = request.Title, Content = request.Content, UserId = userId };
        }

        public static Post DtoToPost(this UpdatePostRequest request, Guid userId)
        {
            return new Post {PostId = request.PostId, Title = request.Title, Content = request.Content, UserId = userId };
        }
    }
}
