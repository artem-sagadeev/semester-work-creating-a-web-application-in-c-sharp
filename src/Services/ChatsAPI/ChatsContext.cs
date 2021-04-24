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
        
        private const string herokuConnectionString =
            "Host=ec2-54-220-170-192.eu-west-1.compute.amazonaws.com;Database=dfe5s5fuj7rt4k;Username=mkvwaakikvpqwe;Password=fc01e7109613356cc6b405f453441d3e95764cfeb50ed8601c60e16db7183798;sslmode=Require;TrustServerCertificate=true";
        public ChatsContext()
        {
        }
        public ChatsContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(herokuConnectionString);
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
