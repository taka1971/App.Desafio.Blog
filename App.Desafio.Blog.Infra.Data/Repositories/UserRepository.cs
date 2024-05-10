using App.Desafio.Blog.Domain.Entities;
using App.Desafio.Blog.Domain.Exceptions;
using App.Desafio.Blog.Domain.Interfaces;
using App.Desafio.Blog.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Desafio.Blog.Infra.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<User> CreateUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetUserByEmailAsync(string email) 
        {
            return await _context.Users.Where(u => u.Email == email).FirstOrDefaultAsync();                            
        }               
    }
}
