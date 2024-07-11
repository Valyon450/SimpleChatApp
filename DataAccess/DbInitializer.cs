using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public static class DbInitializer
    {
        public static void Initialize(SimpleChatDbContext context)
        {
            context.Database.Migrate();
            SeedDatabase(context);
        }

        public static void SeedDatabase(SimpleChatDbContext context)
        {
            // TODO: Seed Database
        }
    }
}
