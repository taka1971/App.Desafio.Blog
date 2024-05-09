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
        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);            

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId);
                entity.Property(e => e.Username).IsRequired().HasMaxLength(70);
                entity.HasIndex(e => e.Email).IsUnique();
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasKey(e => e.PostId);
                entity.Property(e => e.Title).IsRequired();
                entity.Property(e => e.Content).IsRequired();
                entity.Property(e => e.DateCreated);
                entity.HasOne(e => e.User)
                    .WithMany()
                    .HasForeignKey(e => e.UserId)
                    .IsRequired();                
            });
        }
    }
}
