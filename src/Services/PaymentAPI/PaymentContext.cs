using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace PaymentAPI
{
    public class PaymentContext : DbContext
    {
        public PaymentContext(DbContextOptions options) : base(options) { }

        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<Transfer> Transfers { get; set; }
        public DbSet<VirtualPurse> VirtualPurses { get; set; }
        public DbSet<Withdrawal> Withdrawals { get; set; }
        
        public DbSet<AdminPurse> AdminPurses { get; set; }
        public DbSet<StorageOfMoney> StorageOfMoney { get; set; }
    }
}
