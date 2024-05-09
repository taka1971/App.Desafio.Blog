using App.Desafio.Blog.Domain.Entities;
using App.Desafio.Blog.Domain.Interfaces;
using App.Desafio.Blog.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace App.Desafio.Blog.Infra.Data.Repositories
{
    public class BlogRepository : IBlogRepository
    {
        private readonly AppDbContext _context;
        public BlogRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Post> CreatePostAsync(Post post)
        {
            await _context.AddAsync(post);
            await _context.SaveChangesAsync();
            return post;
        }

        public async Task DeletePostAsync(Post post)
        {
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Post>> GetAllPostsAsync()  
            => await _context.Posts.ToListAsync();

        public async Task<IEnumerable<Post>> GetAllPostsByUserIdAsync(Guid userId) 
            => await _context.Posts.Where(p=> p.UserId == userId).ToListAsync();            

        public async Task<Post> UpdatePostAsync(Post post)
        {
            _context.Posts.Update(post);
            await _context.SaveChangesAsync();
            return post;
        }
    }
}
