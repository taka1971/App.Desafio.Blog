using App.Desafio.Blog.Domain.Dtos.Requests;
using App.Desafio.Blog.Domain.Dtos.Responses;
using App.Desafio.Blog.Domain.Entities;
using App.Desafio.Blog.Domain.Enums;
using App.Desafio.Blog.Domain.Exceptions;
using App.Desafio.Blog.Domain.Extensions;
using App.Desafio.Blog.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Desafio.Blog.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly PasswordHasher<User> _passwordHasher = new PasswordHasher<User>();

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserRegisterResponse> CreateUserAsync(UserRegisterRequest request)
        {
            var existingUser = await _userRepository.GetUserByEmailAsync(request.Email);
            var msg = string.Empty;

            if (existingUser != null)
            {
                msg = $"User {request.Email} already exists.";
                throw new DomainException(DomainErrorCode.ValidationFail, msg);
            }

            var newUser = new User
            {
                Username = request.Username,
                Email = request.Email,
                PasswordHash = _passwordHasher.HashPassword(null, request.Password)
            };

            var user = await _userRepository.CreateUserAsync(newUser);

            return user.ToDto();
        }

        public async Task<User> AuthenticateAsync(string email, string password)
        {
            var user = await _userRepository.GetUserByEmailAsync(email) ?? throw new DomainException(DomainErrorCode.NotFound, "User not found.");

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);

            if (result == PasswordVerificationResult.Failed)
            {
                throw new DomainException(DomainErrorCode.ValidationFail,"Invalid password.");
            }

            return user;
        }        

        public async Task<UserRegisterResponse> GetUserByEmailAsync(string email)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);

            return user.ToDto();
        }
    }
}
