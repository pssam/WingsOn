using Microsoft.Extensions.Configuration;

namespace WingsOn.Api.IntegrationXTests
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration) : base(configuration)
        {
            // Do nothing.
        }
    }
}