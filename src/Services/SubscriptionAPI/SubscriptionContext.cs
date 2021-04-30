using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SubscriptionAPI.Entities;

namespace SubscriptionAPI
{
    public class SubscriptionContext : DbContext
    {
        private const string ConnectionString = "Host=localhost;Database=Subscription;Username=postgres;Password=postgres";
        
        private const string HerokuConnectionString =
            "Host=ec2-54-73-58-75.eu-west-1.compute.amazonaws.com;Database=d3b3hl5k84ut1u;Username=lsjfnqjzqgbnxn;Password=925ae08ba83ddaa525d4bab9df92b5de49a38de6713e88eacd907b4de767dd51;sslmode=Require;TrustServerCertificate=true";
        
        public SubscriptionContext()
        {
        }
        public SubscriptionContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(HerokuConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PaidSubscription>()
                .HasKey(c => new {c.UserId, c.SubscribedToId, c.TariffId});
             //
        }

        public DbSet<Tariff> Tariffs { get; set; }
        //public DbSet<TypeOfSubscription> TypeOfSubscriptions { get; set; }
        public DbSet<PaidSubscription> PaidSubscriptions { get; set; }
    }
}
