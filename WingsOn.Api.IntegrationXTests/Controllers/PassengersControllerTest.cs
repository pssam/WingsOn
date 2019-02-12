using System;
using System.Globalization;
using System.Linq;
using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using WingsOn.Domain;
using Xunit;

namespace WingsOn.Api.IntegrationXTests.Controllers
{
    public class PassengersControllerTest : IClassFixture<TestApplicationFactory>
    {
        private readonly WebApplicationFactory<TestStartup> _applicationFactory;

        public PassengersControllerTest(TestApplicationFactory applicationFactory)
        {
            _applicationFactory = applicationFactory;
        }

        [Fact]
        public void Get_ShouldReturnListOfAllPassangers()
        {
            var client = _applicationFactory.CreateClient();

            var response = client.GetAsync("api/passangers").Result;
            var actual = RequestHelper.ReadResponse<Person[]>(response);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(8, actual.Length);
        }

        [Fact]
        public void Get_WhenFiltersArePassed_ShouldFilterPassangers()
        {
            var client = _applicationFactory.CreateClient();

            var response = client.GetAsync("api/passangers?flightNumber=BB124&gender=0").Result;
            var actual = RequestHelper.ReadResponse<Person[]>(response);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            actual.Single().Should().BeEquivalentTo(new Person
            {
                Id = 40,
                Address = "3 Macpherson Junction",
                DateBirth = DateTime.Parse("16/11/1977", new CultureInfo("nl-NL")),
                Gender = GenderType.Male,
                Email = "brice5@hostgator.com",
                Name = "Bonnie Rice"
            });
        }
    }
}