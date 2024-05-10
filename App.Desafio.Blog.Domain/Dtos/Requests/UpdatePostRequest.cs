namespace App.Desafio.Blog.Domain.Dtos.Requests
{
    public record UpdatePostRequest(Guid PostId, string Title, string Content);
}
