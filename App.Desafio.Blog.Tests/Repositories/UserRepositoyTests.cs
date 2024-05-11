using App.Desafio.Blog.Domain.Entities;
using App.Desafio.Blog.Infra.Data.Context;
using App.Desafio.Blog.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace App.Desafio.Blog.Tests.Repositories
{
    public class UserRepositoryTests : IDisposable
    {
        private readonly AppDbContext _context;
        private readonly UserRepository _repository;

        public UserRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) 
                .Options;

            _context = new AppDbContext(options);
            _repository = new UserRepository(_context);
            ClearDatabase().Wait();
        }

        private async Task ClearDatabase()
        {
            _context.Posts.RemoveRange(_context.Posts);
            await _context.SaveChangesAsync();
        }

        [Fact]
        public async Task CreateUserAsync_UserIsAdded()
        {
            // Arrange
            var user = new User { UserId = Guid.NewGuid(), Email = "test@challanger.com", Username = "testuser", PasswordHash = "passhash123" };

            // Act
            var result = await _repository.CreateUserAsync(user);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, await _context.Users.CountAsync());
        }

        [Fact]
        public async Task GetUserByEmailAsync_ReturnsUserIfExists()
        {
            // Arrange
            var user = new User { UserId = Guid.NewGuid(), Email = "test@challanger.com", Username = "testuser", PasswordHash = "passhash123" };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetUserByEmailAsync("test@challanger.com");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.Email, result.Email);
        }

        [Fact]
        public async Task GetUserByEmailAsync_ReturnsNullIfUserDoesNotExist()
        {
            // Arrange           

            // Act
            var result = await _repository.GetUserByEmailAsync("nonexistent@challanger.com");

            // Assert
            Assert.Null(result);
        }

        public void Dispose()
        {
            ClearDatabase().Wait();
            _context.Dispose();
        }
    }

}
