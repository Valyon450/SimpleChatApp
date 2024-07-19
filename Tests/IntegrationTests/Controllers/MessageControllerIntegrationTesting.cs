using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace IntegrationTests.Controllers
{
    public class MessageControllerIntegrationTesting : IntegrationTesting
    {
        public MessageControllerIntegrationTesting(CustomWebFactory<Program> factory) : base(factory)
        {
        }
    }
}
