using DataAccess;
using WebApi.DI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureDependency(builder.Configuration);

var app = builder.Build();

// Initialize the database
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<SimpleChatDbContext>();
    DbInitializer.Initialize(context);
}

app.Run();
