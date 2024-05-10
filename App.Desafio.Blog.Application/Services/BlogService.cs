using App.Desafio.Blog.Domain.Dtos.Requests;
using App.Desafio.Blog.Domain.Dtos.Responses;
using App.Desafio.Blog.Domain.Enums;
using App.Desafio.Blog.Domain.Exceptions;
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

        public async Task DeletePostAsync(Guid id, Guid userId)
        {
            var post = await _blogRepository.GetPostByUserIdAndPostId(id, userId);
            
            await _blogRepository.DeletePostAsync(post);
        }

        public async Task<IEnumerable<PostResponse>> GetAllPostsAsync()
        {
            var posts = await _blogRepository.GetAllPostsAsync();
            var response = posts.Select(post => new PostResponse
            (
                post.PostId,
                post.Title,
                post.Content,
                post.DateCreated,
                post.UserId
            )).ToList();

            return response;

        }

        public async Task<IEnumerable<PostResponse>> GetAllPostsByUserIdAsync(Guid userId)
        {
            var posts = await _blogRepository.GetAllPostsByUserIdAsync(userId);
            var response = posts.Select(post => new PostResponse
            (
                post.PostId,
                post.Title,
                post.Content,
                post.DateCreated,
                post.UserId
            )).ToList();

            return response;
        }

        public async Task<PostResponse> UpdatePostAsync(UpdatePostRequest request, Guid userId)
        {
            await _blogRepository.GetPostByUserIdAndPostId(request.PostId, userId);

            var post = request.DtoToPost(userId);
            var response = await _blogRepository.UpdatePostAsync(post);
            return post.DtoToPost();
        }
    }
}
