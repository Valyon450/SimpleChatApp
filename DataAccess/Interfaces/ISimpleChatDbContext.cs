using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Interfaces
{
    public interface ISimpleChatDbContext
    {
        DbSet<User> User { get; }
        DbSet<Chat> Chat { get; }
        DbSet<Message> Message { get; }
        DbSet<UserChat> UserChat { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
