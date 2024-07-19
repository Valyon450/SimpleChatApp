using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace IntegrationTests.Controllers
{
    public class UserControllerIntegrationTesting : IntegrationTesting
    {
        public UserControllerIntegrationTesting(CustomWebFactory<Program> factory) : base(factory)
        {
        }
    }
}
