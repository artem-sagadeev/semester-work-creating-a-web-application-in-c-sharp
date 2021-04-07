using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Areas.Identity.Data
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        private const string ConnectionString = "Host=localhost;Database=identity_db;Username=postgres;Password=qweasd123";

        private const string HerokuConnectionString = "Host=ec2-54-74-77-126.eu-west-1.compute.amazonaws.com;Database=d9umm2cnp2i9fc;Username=speaobxrqbrqel;Password=1a691003ab0ed12c836a33c97ab2ee75137ac8057f72a85968e9d39d49d43d30;sslmode=Require;TrustServerCertificate=true";

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
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
    }
}
