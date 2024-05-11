using NSubstitute;
using Shouldly;
using App.Desafio.Blog.Domain.Interfaces;
using App.Desafio.Blog.Application.Services;
using App.Desafio.Blog.Domain.Entities;
using App.Desafio.Blog.Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using App.Desafio.Blog.Domain.Dtos.Requests;
using App.Desafio.Blog.Domain.Enums;

namespace App.Desafio.Blog.Tests.Services
{
    public class UserServiceTests
    {
        private readonly IUserRepository _userRepository = Substitute.For<IUserRepository>();
        private readonly UserService _userService;
        private readonly PasswordHasher<User> _passwordHasher = new PasswordHasher<User>();

        public UserServiceTests()
        {
            _userService = new UserService(_userRepository);
        }

        [Fact]
        public async Task CreateUserAsync_UserExists_ThrowsDomainException()
        {
            // Arrange
            var request = new UserRegisterRequest ( "testuser", "test@challenger.com", "password123" );
            _userRepository.GetUserByEmailAsync(request.Email).Returns(Task.FromResult(new User()));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<DomainException>(() => _userService.CreateUserAsync(request));
            exception.ErrorCode.ShouldBe(DomainErrorCode.ValidationFail);
        }

        [Fact]
        public async Task CreateUserAsync_UserNotExists_CreatesUser()
        {
            // Arrange
            var request = new UserRegisterRequest("testuser", "test@challenger.com", "password123");
            _userRepository.GetUserByEmailAsync(request.Email).Returns(Task.FromResult<User>(null));
            _userRepository.CreateUserAsync(Arg.Any<User>()).Returns(Task.FromResult(new User { Username = request.Username, Email = request.Email }));

            // Act
            var result = await _userService.CreateUserAsync(request);

            // Assert
            result.ShouldNotBeNull();
            result.Email.ShouldBe(request.Email);
            await _userRepository.Received(1).CreateUserAsync(Arg.Any<User>());
        }
    }
}
