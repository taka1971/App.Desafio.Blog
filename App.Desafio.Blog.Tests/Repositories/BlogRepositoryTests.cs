using App.Desafio.Blog.Domain.Entities;
using App.Desafio.Blog.Infra.Data.Context;
using App.Desafio.Blog.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace App.Desafio.Blog.Tests.Repositories
{
    public class BlogRepositoryTests: IDisposable
    {
        private readonly AppDbContext _context;
        private readonly BlogRepository _repository;

        public BlogRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "ChallangerDbTest")
                .Options;

            _context = new AppDbContext(options);
            _repository = new BlogRepository(_context);

            ClearDatabase().Wait();
        }

        private async Task ClearDatabase()
        {
            _context.Posts.RemoveRange(_context.Posts);
            await _context.SaveChangesAsync();
        }

        [Fact]
        public async Task DeletePostAsync_PostIsDeleted()
        {
            // Arrange            

            var post = new Post { PostId = Guid.NewGuid(), UserId = Guid.NewGuid(), Title = "Test Title", Content = "Test content" };
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            // Act
            await _repository.DeletePostAsync(post);

            // Assert
            Assert.Equal(0, _context.Posts.Count());
        }

        [Fact]
        public async Task CreatePostAsync_PostIsAdded()
        {
            // Arrange
            var post = new Post { PostId = Guid.NewGuid(), UserId = Guid.NewGuid(), Title = "Test Title", Content = "Test content" };

            // Act
            var result = await _repository.CreatePostAsync(post);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, _context.Posts.Count());
        }


        [Fact]
        public async Task GetAllPostsAsync_ReturnsAllPosts()
        {
            // Arrange
            _context.Posts.AddRange(
                new Post { PostId = Guid.NewGuid(), Title = "Test Title 1", Content = "Content 1", UserId = Guid.NewGuid() },
                new Post { PostId = Guid.NewGuid(), Title = "Test Title 2", Content = "Content 2", UserId = Guid.NewGuid() }
            );
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetAllPostsAsync();

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetAllPostsByUserIdAsync_ReturnsPostsForUser()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _context.Posts.AddRange(
                new Post { PostId = Guid.NewGuid(), Title = "Test Title 1", Content = "Content 1", UserId = userId },
                new Post { PostId = Guid.NewGuid(), Title = "Test Title 2", Content = "Content 2", UserId = Guid.NewGuid() }  
            );
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetAllPostsByUserIdAsync(userId);

            // Assert
            Assert.Single(result);
        }

        [Fact]
        public async Task GetPostByUserIdAndPostId_ReturnsCorrectPost()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var postId = Guid.NewGuid();
            _context.Posts.Add(new Post { PostId = postId, UserId = userId, Title = "Test Title", Content = "Test content" });
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetPostByUserIdAndPostId(postId, userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(postId, result.PostId);
            Assert.Equal(userId, result.UserId);
        }

        [Fact]
        public async Task UpdatePostAsync_UpdatesPostSuccessfully()
        {
            // Arrange
            var post = new Post { PostId = Guid.NewGuid(), UserId = Guid.NewGuid(), Title = "Old Title", Content = "Old content" };
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            // Act
            post.Title = "New Title";
            var result = await _repository.UpdatePostAsync(post);

            // Assert
            Assert.Equal("New Title", result.Title);
            Assert.Equal("New Title", _context.Posts.First().Title);
        }

        public void Dispose()
        {
            ClearDatabase().Wait();
            _context.Dispose();
        }
    }

}
