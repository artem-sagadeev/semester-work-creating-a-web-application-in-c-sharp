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
        public ChatsContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<ChatMember>()
                .HasKey(c => new { c.UserId, c.ProjectId });
        }

        public DbSet<ChatMember> ChatsMembers { get; set; }
        public DbSet<Message> Messages { get; set; }
    }
}
