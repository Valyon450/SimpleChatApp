namespace DataAccess
{
    public static class DbInitializer
    {
        public static void Initialize(SimpleChatDbContext context)
        {
            context.Database.EnsureCreated();
            SeedDatabase(context);
        }

        public static void SeedDatabase(SimpleChatDbContext context)
        {
            // TODO: Seed Database
        }
    }
}
