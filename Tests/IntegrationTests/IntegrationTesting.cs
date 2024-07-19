using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using DataAccess;

namespace IntegrationTests
{
    public abstract class IntegrationTesting : IClassFixture<CustomWebFactory<Program>>
    {
        protected readonly CustomWebFactory<Program> factory;
        protected readonly SimpleChatDbContext context;
        protected readonly HttpClient client;
        protected readonly IConfiguration configuration;

        public IntegrationTesting(CustomWebFactory<Program> factory)
        {
            this.factory = factory;

            var scope = this.factory.Services.CreateScope();

            this.context = scope.ServiceProvider.GetRequiredService<SimpleChatDbContext>();

            this.configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

            this.client = this.factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }
    }
}
