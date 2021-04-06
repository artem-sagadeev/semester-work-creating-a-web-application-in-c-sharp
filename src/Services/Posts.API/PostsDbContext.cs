using Microsoft.EntityFrameworkCore;
using Posts.API.Entities;

namespace Posts.API
{
    public class PostsDbContext : DbContext
    {
        private const string ConnectionString = "Host=localhost;Database=posts_db;Username=postgres;Password=qweasd123";
        
        private const string HerokuConnectionString =
            "Host=ec2-54-155-35-88.eu-west-1.compute.amazonaws.com;Database=dbficjagcpad5a;Username=tgmyqvmaajvbtg;Password=b5e05f010fb02c7070d51af7039aae2285ac0632b699139c6cdef734f36ef61f;sslmode=Require;TrustServerCertificate=true";

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
                optionsBuilder.UseNpgsql(HerokuConnectionString);
            }
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Russian_Russia.1251");
        }
    }
}