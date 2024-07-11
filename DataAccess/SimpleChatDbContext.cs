using DataAccess.Entities;
using DataAccess.EntityTypeConfigurations;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class SimpleChatDbContext : DbContext, ISimpleChatDbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Chat> Chat { get; set; }
        public DbSet<Message> Message { get; set; }
        public DbSet<UserChat> UserChat { get; set; }

        public SimpleChatDbContext(DbContextOptions<SimpleChatDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new ChatConfiguration());
            modelBuilder.ApplyConfiguration(new MessageConfiguration());
            modelBuilder.ApplyConfiguration(new UserChatConfiguration());
        }
    }
}
