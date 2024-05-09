using App.Desafio.Blog.Domain.Dtos.Requests;
using App.Desafio.Blog.Domain.Dtos.Responses;

namespace App.Desafio.Blog.Domain.Interfaces
{
    public interface IBlogService
    {
        Task<IEnumerable<PostResponse>> GetAllPostsAsync();
        Task<IEnumerable<PostResponse>> GetAllPostsByUserIdAsync(Guid userId);
        Task<PostResponse> CreatePostAsync(CreatePostRequest request, Guid userId);
        Task<PostResponse> UpdatePostAsync(UpdatePostRequest request, Guid userId);
        Task<bool> DeletePostAsync(Guid id, Guid userId);
    }
}
