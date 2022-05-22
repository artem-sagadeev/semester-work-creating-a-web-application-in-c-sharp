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
        public SubscriptionContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PaidSubscription>().HasKey(c => new {c.UserId, c.SubscribedToId, c.TariffId});
        }

        public DbSet<Tariff> Tariffs { get; set; }
        public DbSet<PaidSubscription> PaidSubscriptions { get; set; }
    }
}
