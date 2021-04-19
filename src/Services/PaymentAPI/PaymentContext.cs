using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace PaymentAPI
{
    public class PaymentContext : DbContext
    {
        private string connectionString = "Host=localhost;Database=Payment;Username=postgres;Password=postgres";
        public PaymentContext()
        {
        }
        public PaymentContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(connectionString);
        }

        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<Transfer> Transfers { get; set; }
        public DbSet<VirtualPurse> VirtualPurses { get; set; }
        public DbSet<Withdrawal> Withdrawals { get; set; }
        
        public DbSet<AdminPurse> AdminPurses { get; set; }
        public DbSet<StorageOfMoney> StorageOfMoney { get; set; }


    }
}
