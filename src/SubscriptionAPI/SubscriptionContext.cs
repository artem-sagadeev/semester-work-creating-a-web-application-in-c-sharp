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
        private string connectionString = "Host=localhost;Database=Subscription;Username=postgres;Password=postgres";
        public SubscriptionContext()
        {
        }
        public SubscriptionContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PaidSubscription>()
                .HasKey(c => new {c.UserId, c.SubscribedToId, c.TariffId});
             //
        }

        public DbSet<Tariff> Tariffs { get; set; }
        public DbSet<TypeOfSubscription> TypeOfSubscriptions { get; set; }
        public DbSet<PaidSubscription> PaidSubscriptions { get; set; }
    }
}
