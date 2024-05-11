using NSubstitute;
using Shouldly;
using Microsoft.AspNetCore.SignalR;
using App.Desafio.Blog.Domain.Dtos.Requests;
using App.Desafio.Blog.Domain.Interfaces;
using App.Desafio.Blog.Crosscutting.Sockets;
using App.Desafio.Blog.Application.Services;
using App.Desafio.Blog.Domain.Dtos.Responses;
using App.Desafio.Blog.Domain.Entities;

namespace App.Desafio.Blog.Tests.Services
{
    public class BlogServiceTests
    {
        private readonly IBlogRepository _blogRepository = Substitute.For<IBlogRepository>();
        private readonly Microsoft.AspNetCore.SignalR.IHubContext<BlogChannel> _hubContext = Substitute.For<Microsoft.AspNetCore.SignalR.IHubContext<BlogChannel>>();
        private readonly BlogService _blogService;

        public BlogServiceTests()
        {
            _blogService = new BlogService(_blogRepository, _hubContext);
        }

        [Fact]
        public async Task CreatePostAsync_ShouldCallRepository()
        {
            // Arrange
            var request = new CreatePostRequest ( "Test title", "Test content" );
            var userId = Guid.NewGuid();
            var expectedPost = new Post { PostId = Guid.NewGuid(), Title = "Test title", Content = "Test content", UserId = userId };
            _blogRepository.CreatePostAsync(Arg.Any<Post>()).Returns(Task.FromResult(expectedPost));

            // Act
            var result = await _blogService.CreatePostAsync(request, userId);

            // Assert
            result.ShouldNotBeNull();
            result.Title.ShouldBe(request.Title);
            result.Content.ShouldBe(request.Content);
            await _blogRepository.Received(1).CreatePostAsync(Arg.Any<Post>());            
        }
    }
}
