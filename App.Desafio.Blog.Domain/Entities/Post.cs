using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Desafio.Blog.Domain.Entities
{
    public class Post
    {
        public Guid PostId { get; set; } = Guid.NewGuid();
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
