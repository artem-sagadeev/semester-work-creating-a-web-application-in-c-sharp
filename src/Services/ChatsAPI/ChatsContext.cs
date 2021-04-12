using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatsAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChatsAPI
{
    public class ChatsContext : DbContext
    {
        private string connectionString = "Host=localhost;Database=Chats;Username=postgres;Password=postgres";
        public ChatsContext()
        {
        }
        public ChatsContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChatMember>()
                .HasKey(c => new { c.UserId, c.ProjectId });
        }

        public DbSet<ChatMember> ChatsMembers { get; set; }
        public DbSet<Message> Messages { get; set; }
    }
}
