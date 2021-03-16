using Microsoft.EntityFrameworkCore;
using Developer.API.Entities;

namespace Developer.API
{
    public class DeveloperDbContext : DbContext
    {
        private const string ConnectionString =
            "Host=localhost;Database=developers_db;Username=postgres;Password=qweasd123";
        
        public DbSet<User> User { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<Project> Project { get; set; }
        
        public DeveloperDbContext()
        {
        }

        public DeveloperDbContext(DbContextOptions options)
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