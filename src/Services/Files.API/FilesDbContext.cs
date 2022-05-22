using Files.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Files.API
{
    public class FilesDbContext : DbContext
    {
        public DbSet<Avatar> Avatar { get; set; }
        public DbSet<Cover> Cover { get; set; }
        public DbSet<File> File { get; set; }

        public FilesDbContext(DbContextOptions options) : base(options) { }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Russian_Russia.1251");
        }
    }
}