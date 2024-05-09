namespace App.Desafio.Blog.Domain.Dtos.Requests
{
    public record UpdatePostRequest(Guid Id, string Title, string Content);
}
