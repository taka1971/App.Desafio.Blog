using App.Desafio.Blog.Domain.Dtos.Responses;
using App.Desafio.Blog.Domain.Entities;

namespace App.Desafio.Blog.Domain.Extensions
{
    public static class PostExtensions
    {
        public static PostResponse DtoToPost(this Post post)
        {
            return new PostResponse(post.PostId, post.Title, post.Content, post.DateCreated);
        }
    }
}
