using NSubstitute;
using Microsoft.Extensions.Options;
using App.Desafio.Blog.Application.Services;
using App.Desafio.Blog.Domain.Entities;

namespace App.Desafio.Blog.Tests.Services
{
    public class TokenServiceTests
    {
        private readonly IOptions<AppSettings> _appSettings = Substitute.For<IOptions<AppSettings>>();
        private readonly TokenService _tokenService;

        public TokenServiceTests()
        {
            _appSettings.Value.Returns(new AppSettings
            {
                JwtSettings = new JwtSettings
                {
                    Secret = "XB3jzEPPwZRo634JM8b-9Kn1gC_LLIg2MyXsw6cMfLs", 
                    Issuer = "Issuer",
                    Audience = "Audience",
                    ExpiresInMinutes = 60
                }
            });

            _tokenService = new TokenService(_appSettings);
        }

        [Fact]
        public async Task GenerateJwtTokenAsync_ReturnsValidToken()
        {
            // Arrange
            var user = new User { UserId = Guid.NewGuid(), Email = "test@challenger.com" };

            // Act
            var result = await _tokenService.GenerateJwtTokenAsync(user);

            // Assert
            Assert.NotNull(result);
            Assert.False(string.IsNullOrWhiteSpace(result.AccessToken));            
        }
    }

}
