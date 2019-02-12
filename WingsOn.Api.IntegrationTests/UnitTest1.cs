using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestPlatform.Common.DataCollection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WingsOn.Domain;

namespace WingsOn.Api.IntegrationTests
{
    [TestClass]
    public class UnitTest1
    {
        private readonly WebApplicationFactory<TestStrartup> _applicationFactory;

        public UnitTest1()
        {
            _applicationFactory = new TestApplicationFactory();
        }

        [TestMethod]
        public void TestMethod1()
        {
            var client = _applicationFactory.CreateClient();

            var response = client.GetAsync("/persons");
            var actual = ReadResponse<Person[]>(response);

            Assert.AreEqual(HttpStatusCode.OK, response.Status);
            Assert.AreEqual(11, actual.Length);
        }

        private static TExpected ReadResponse<TExpected>(Task<HttpResponseMessage> response)
        {
            var responseContent = response.Result.Content.ReadAsStringAsync().Result;
            var actual = JsonConvert.DeserializeObject<TExpected>(responseContent);
            return actual;
        }
    }

    public class TestApplicationFactory : WebApplicationFactory<TestStrartup>
    {

    }


    public class TestStrartup : Startup
    {
        public TestStrartup(IConfiguration configuration) : base(configuration)
        {
            // Do nothing.
        }
    }

}
