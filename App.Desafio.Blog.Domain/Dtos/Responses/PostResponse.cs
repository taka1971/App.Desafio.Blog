
namespace App.Desafio.Blog.Domain.Dtos.Responses
{
    public record PostResponse(Guid Id, string Title, string Content, DateTime DateCreated);
}
