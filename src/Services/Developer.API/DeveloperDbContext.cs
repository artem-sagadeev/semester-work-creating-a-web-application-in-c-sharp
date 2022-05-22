using Microsoft.EntityFrameworkCore;
using Developer.API.Entities;

namespace Developer.API
{
    public class DeveloperDbContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<Tag> Tag { get; set; }

        public DeveloperDbContext(DbContextOptions options) : base(options) { }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Russian_Russia.1251");
        }
    }
}