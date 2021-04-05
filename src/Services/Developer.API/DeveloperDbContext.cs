using Microsoft.EntityFrameworkCore;
using Developer.API.Entities;

namespace Developer.API
{
    public class DeveloperDbContext : DbContext
    {
        private const string ConnectionString =
            "Host=localhost;Database=developers_db;Username=postgres;Password=qweasd123";
        
        private const string HerokuConnectionString =
            "Host=ec2-54-155-35-88.eu-west-1.compute.amazonaws.com;Database=d8vps29lbcia1r;Username=fytxjwvkkjpewd;Password=1ce4e98e2386cd5e64009fd497bf29f7fc68c309eda345bdf992e22fe40630eb;sslmode=Require;TrustServerCertificate=true";
        
        public DbSet<User> User { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<Tag> Tag { get; set; }
        
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
                optionsBuilder.UseNpgsql(HerokuConnectionString);
            }
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Russian_Russia.1251");
        }
    }
}