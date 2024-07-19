using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public static class DbInitializer
    {
        public static void Initialize(SimpleChatDbContext context)
        {
            if (context.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
            {
                context.Database.Migrate();
            }

            // SeedDatabase(context);
        }

        public static void SeedDatabase(SimpleChatDbContext context)
        {
            var users = new List<User>
            {
                new User { UserName = "Alice" },
                new User { UserName = "Bob" },
                new User { UserName = "Charlie" },
                new User { UserName = "Dave" },
                new User { UserName = "Eve" },
                new User { UserName = "Frank" },
                new User { UserName = "Grace" },
                new User { UserName = "Hank" },
                new User { UserName = "Ivy" },
                new User { UserName = "Jack" },
                new User { UserName = "Karen" },
                new User { UserName = "Leo" },
                new User { UserName = "Mona" },
                new User { UserName = "Nate" },
                new User { UserName = "Olivia" }
            };

            context.User.AddRange(users);
            context.SaveChanges();
                        
            var chats = new List<Chat>
            {
                new Chat { Name = "General", CreatedBy = users[0], CreatedById = users[0].Id },
                new Chat { Name = "Development", CreatedBy = users[1], CreatedById = users[1].Id },
                new Chat { Name = "Marketing", CreatedBy = users[2], CreatedById = users[2].Id },
                new Chat { Name = "Sales", CreatedBy = users[3], CreatedById = users[3].Id },
                new Chat { Name = "Support", CreatedBy = users[4], CreatedById = users[4].Id },
                new Chat { Name = "HR", CreatedBy = users[5], CreatedById = users[5].Id },
                new Chat { Name = "Finance", CreatedBy = users[6], CreatedById = users[6].Id },
                new Chat { Name = "Operations", CreatedBy = users[7], CreatedById = users[7].Id },
                new Chat { Name = "Admin", CreatedBy = users[8], CreatedById = users[8].Id },
                new Chat { Name = "Projects", CreatedBy = users[9], CreatedById = users[9].Id }
            };

            context.Chat.AddRange(chats);
            context.SaveChanges();

            var messages = new List<Message>
            {
                new Message { Text = "Hello, everyone!", Chat = chats[0], ChatId = chats[0].Id, User = users[0], UserId = users[0].Id },
                new Message { Text = "Good morning!", Chat = chats[0], ChatId = chats[0].Id, User = users[1], UserId = users[1].Id },
                new Message { Text = "Hi!", Chat = chats[1], ChatId = chats[1].Id, User = users[2], UserId = users[2].Id },
                new Message { Text = "Good afternoon!", Chat = chats[1], ChatId = chats[1].Id, User = users[3], UserId = users[3].Id },
                new Message { Text = "How can I help?", Chat = chats[2], ChatId = chats[2].Id, User = users[4], UserId = users[4].Id },
                new Message { Text = "Need assistance?", Chat = chats[2], ChatId = chats[2].Id, User = users[5], UserId = users[5].Id },
                new Message { Text = "What's up?", Chat = chats[3], ChatId = chats[3].Id, User = users[6], UserId = users[6].Id },
                new Message { Text = "Hello!", Chat = chats[3], ChatId = chats[3].Id, User = users[7], UserId = users[7].Id },
                new Message { Text = "Meeting at 3 PM", Chat = chats[4], ChatId = chats[4].Id, User = users[8], UserId = users[8].Id },
                new Message { Text = "Check your email", Chat = chats[4], ChatId = chats[4].Id, User = users[9], UserId = users[9].Id },
                new Message { Text = "Happy Monday!", Chat = chats[5], ChatId = chats[5].Id, User = users[10], UserId = users[10].Id },
                new Message { Text = "Good news!", Chat = chats[5], ChatId = chats[5].Id, User = users[11], UserId = users[11].Id },
                new Message { Text = "Welcome aboard!", Chat = chats[6], ChatId = chats[6].Id, User = users[12], UserId = users[12].Id },
                new Message { Text = "Hi, new here!", Chat = chats[6], ChatId = chats[6].Id, User = users[13], UserId = users[13].Id },
                new Message { Text = "Let's discuss", Chat = chats[7], ChatId = chats[7].Id, User = users[14], UserId = users[14].Id }
            };

            context.Message.AddRange(messages);
            context.SaveChanges();
                        
            var userChats = new List<UserChat>
            {
                new UserChat { UserId = users[0].Id, ChatId = chats[0].Id },
                new UserChat { UserId = users[1].Id, ChatId = chats[0].Id },
                new UserChat { UserId = users[2].Id, ChatId = chats[1].Id },
                new UserChat { UserId = users[3].Id, ChatId = chats[1].Id },
                new UserChat { UserId = users[4].Id, ChatId = chats[2].Id },
                new UserChat { UserId = users[5].Id, ChatId = chats[2].Id },
                new UserChat { UserId = users[6].Id, ChatId = chats[3].Id },
                new UserChat { UserId = users[7].Id, ChatId = chats[3].Id },
                new UserChat { UserId = users[8].Id, ChatId = chats[4].Id },
                new UserChat { UserId = users[9].Id, ChatId = chats[4].Id },
                new UserChat { UserId = users[10].Id, ChatId = chats[5].Id },
                new UserChat { UserId = users[11].Id, ChatId = chats[5].Id },
                new UserChat { UserId = users[12].Id, ChatId = chats[6].Id },
                new UserChat { UserId = users[13].Id, ChatId = chats[6].Id },
                new UserChat { UserId = users[14].Id, ChatId = chats[7].Id }
            };

            context.UserChat.AddRange(userChats);
            context.SaveChanges();
        }
    }
}
