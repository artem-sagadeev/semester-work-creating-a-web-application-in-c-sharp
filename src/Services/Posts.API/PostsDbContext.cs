using Microsoft.EntityFrameworkCore;
using Posts.API.Entities;

namespace Posts.API
{
    public class PostsDbContext : DbContext
    {
        private const string ConnectionString = "Host=localhost;Database=posts_db;Username=postgres;Password=qweasd123";
        
        public DbSet<Post> Post { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<File> File { get; set; }
        
        public PostsDbContext()
        {
        }

        public PostsDbContext(DbContextOptions options)
            : base(options)
        {
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(ConnectionString);
            }
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Russian_Russia.1251");
        }
    }
}