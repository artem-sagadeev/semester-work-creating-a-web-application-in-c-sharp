using Microsoft.EntityFrameworkCore;
using Posts.API.Entities;

namespace Posts.API
{
    public class PostsDbContext : DbContext
    {
        public DbSet<Post> Post { get; set; }
        public DbSet<Comment> Comment { get; set; }
        
        public PostsDbContext()
        {
        }

        public PostsDbContext(DbContextOptions options) : base(options) { }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Russian_Russia.1251");
        }
    }
}