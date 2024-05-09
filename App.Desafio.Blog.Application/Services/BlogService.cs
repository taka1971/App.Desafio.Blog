using App.Desafio.Blog.Domain.Dtos.Requests;
using App.Desafio.Blog.Domain.Dtos.Responses;
using App.Desafio.Blog.Domain.Extensions;
using App.Desafio.Blog.Domain.Interfaces;

namespace App.Desafio.Blog.Application.Services
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepository;
        public BlogService(IBlogRepository blogRepository)
        { 
            _blogRepository = blogRepository;
        }
        public async Task<PostResponse> CreatePostAsync(CreatePostRequest request, Guid userId)
        {
            var post = request.DtoToPost(userId);
            var response = await _blogRepository.CreatePostAsync(post);
            return post.DtoToPost();
        }

        public Task<bool> DeletePostAsync(Guid id, Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PostResponse>> GetAllPostsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PostResponse>> GetAllPostsByUserIdAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task<PostResponse> UpdatePostAsync(UpdatePostRequest request, Guid userId)
        {
            var post = request.DtoToPost(userId);
            var response = await _blogRepository.UpdatePostAsync(post);
            return post.DtoToPost();
        }
    }
}
