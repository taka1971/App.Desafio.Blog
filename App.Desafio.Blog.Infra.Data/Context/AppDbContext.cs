using App.Desafio.Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.Desafio.Blog.Infra.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);            

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Username).IsRequired().HasMaxLength(70);
                entity.HasIndex(e => e.Email).IsUnique();
            });
        }
    }
}
