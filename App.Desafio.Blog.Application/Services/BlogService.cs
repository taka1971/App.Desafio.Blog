using App.Desafio.Blog.Crosscutting.Sockets;
using App.Desafio.Blog.Domain.Dtos.Requests;
using App.Desafio.Blog.Domain.Dtos.Responses;
using App.Desafio.Blog.Domain.Enums;
using App.Desafio.Blog.Domain.Exceptions;
using App.Desafio.Blog.Domain.Extensions;
using App.Desafio.Blog.Domain.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace App.Desafio.Blog.Application.Services
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IHubContext<BlogChannel> _hubContext;
        public BlogService(IBlogRepository blogRepository, IHubContext<BlogChannel> hubContext)
        { 
            _blogRepository = blogRepository;
            _hubContext = hubContext;
        }
        public async Task<PostResponse> CreatePostAsync(CreatePostRequest request, Guid userId)
        {
            var post = request.DtoToPost(userId);
            var response = await _blogRepository.CreatePostAsync(post);

            await _hubContext.Clients.All.SendAsync("ReceiveMessage", "A new post has been created!");

            return post.DtoToPost();
        }

        public async Task DeletePostAsync(Guid id, Guid userId)
        {
            var post = await _blogRepository.GetPostByUserIdAndPostId(id, userId);

            if (post == null) {
                throw new DomainException(DomainErrorCode.NotFound, "Post not found."); 
            }
            
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
            var post = await _blogRepository.GetPostByUserIdAndPostId(request.PostId, userId);

            if (post is null)
                throw new DomainException(DomainErrorCode.NotFound, "Post not found.");

            post.Content = request.Content;
            post.Title = request.Title;
            
            var response = await _blogRepository.UpdatePostAsync(post);
            return post.DtoToPost();
        }
    }
}
