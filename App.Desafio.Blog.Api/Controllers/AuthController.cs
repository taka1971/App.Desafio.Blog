using App.Desafio.Blog.Domain.Dtos.Requests;
using App.Desafio.Blog.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace App.Desafio.Blog.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthController : BaseController
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public AuthController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        /// <summary>
        /// Register one new user.
        /// </summary>
        /// <remarks>
        /// Use this feature to create a new user. All new users will be deliverer.
        /// </remarks>
        /// <response code="200">Success register user.</response>
        /// <response code="400">Fail validation.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequest request)
        {
            var user = await _userService.CreateUserAsync(request);
            var message = $"Success. User {user.Email} registered.";

            return ApiResponse(message, user);
        }

        /// <summary>
        /// Login user.
        /// </summary>
        /// <remarks>
        /// Use this feature to validate user access. If successful, a token will be returned.
        /// This token will be used to access the other microservices.
        /// </remarks>
        /// <response code="200">Success login.</response>
        /// <response code="400">Fail validation. </response>
        /// <response code="401">User unauthorized. Check if the user exists and if their data has been entered correctly. </response>
        /// <response code="500">Internal server error.</response>
        [HttpGet("login")]
        public async Task<IActionResult> Login([FromQuery] UserLoginRequest request)
        {
            var user = await _userService.AuthenticateAsync(request.Email, request.Password);
            var token = await _tokenService.GenerateJwtTokenAsync(user);
            var message = $"Success. Generate Token from User {request.Email}.";

            return ApiResponse(message, token);
        }

        /// <summary>
        /// Get user by email.
        /// </summary>        
        /// <response code="200">Success query.</response>
        /// <response code="400">Fail validation. </response>
        /// <response code="404">User not found. </response>
        /// <response code="500">Internal server error.</response>
        [HttpGet("user")]
        public async Task<IActionResult> GetUser([FromQuery] string request)
        {
            var user = await _userService.GetUserByEmailAsync(request);

            return ApiResponse(user);
        }
    }
}
