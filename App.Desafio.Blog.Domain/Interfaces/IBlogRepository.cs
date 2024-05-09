using App.Desafio.Blog.Domain.Entities;

namespace App.Desafio.Blog.Domain.Interfaces
{
    public interface IBlogRepository
    {
        Task<IEnumerable<Post>> GetAllPostsAsync();
        Task<IEnumerable<Post>> GetAllPostsByUserIdAsync(Guid userId);
        Task<Post> CreatePostAsync(Post post);
        Task<Post> UpdatePostAsync(Post post);
        Task DeletePostAsync(Post post);
    }
}
