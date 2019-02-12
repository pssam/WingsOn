using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace WingsOn.Api.IntegrationXTests
{
    public static class RequestHelper
    {
        public static StringContent CreateContent<TContent>(TContent parameter)
        {
            var content = new StringContent(JsonConvert.SerializeObject(parameter), Encoding.UTF8, "application/json");
            return content;
        }

        public static TExpected ReadResponse<TExpected>(HttpResponseMessage response)
        {
            var responseContent = response.Content.ReadAsStringAsync().Result;
            var actual = JsonConvert.DeserializeObject<TExpected>(responseContent);
            return actual;
        }
    }
}