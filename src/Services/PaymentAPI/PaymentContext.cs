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
        private const string herokuConnectionString =
            "Host=ec2-54-228-99-58.eu-west-1.compute.amazonaws.com;Database=d65q3na9gpsutg;Username=nakagyopbcuoij;Password=c769d68ae166b56b6eeccd8b03034bc3187ede73d6e3953b5a86f6747fdcbccd;sslmode=Require;TrustServerCertificate=true";
        public PaymentContext()
        {
        }
        public PaymentContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(herokuConnectionString);
        }

        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<Transfer> Transfers { get; set; }
        public DbSet<VirtualPurse> VirtualPurses { get; set; }
        public DbSet<Withdrawal> Withdrawals { get; set; }
        
        public DbSet<AdminPurse> AdminPurses { get; set; }
        public DbSet<StorageOfMoney> StorageOfMoney { get; set; }


    }
}
