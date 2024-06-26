﻿namespace App.Desafio.Blog.Domain.Entities
{
    public class User
    {
        public Guid UserId { get; set; } = Guid.NewGuid();

        public string Username { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
        
        public string PasswordHash { get; set; } = string.Empty;
    }
}
