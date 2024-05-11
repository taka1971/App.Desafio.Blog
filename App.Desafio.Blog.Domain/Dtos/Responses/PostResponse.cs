
namespace App.Desafio.Blog.Domain.Dtos.Responses
{
    public record PostResponse(Guid PostId, string Title, string Content, DateTime DateCreated, Guid UserId);
}
