using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using WingsOn.Api.ViewModels;
using WingsOn.Dal;
using WingsOn.Domain;
using Xunit;

namespace WingsOn.Api.IntegrationXTests.Controllers
{
    public class PersonsControllerTest:IClassFixture<TestApplicationFactory>
    {
        private readonly WebApplicationFactory<TestStartup> _applicationFactory;

        public PersonsControllerTest(TestApplicationFactory applicationFactory)
        {
            _applicationFactory = applicationFactory;
        }

        [Fact]
        public void Get_ShouldReturnListOfPersons()
        {
            var client = _applicationFactory.CreateClient();

            var response = client.GetAsync("api/persons").Result;
            var actual = RequestHelper.ReadResponse<Person[]>(response);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(11, actual.Length);
        }

        [Fact]
        public void GetPerson_WhenPersonExists_ShouldReturnPerson()
        {
            var client = _applicationFactory.CreateClient();

            var response = client.GetAsync("api/persons/91").Result;
            var actual = RequestHelper.ReadResponse<Person>(response);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            actual.Should().BeEquivalentTo(new Person
            {
                Id = 91,
                Address = "805-1408 Mi Rd.",
                DateBirth = DateTime.Parse("24/09/1980", new CultureInfo("nl-NL")),
                Email = "egestas.a.dui@aliquet.ca",
                Gender = GenderType.Male,
                Name = "Kendall Velazquez"
            });
        }

        [Fact]
        public void GetPerson_WhenPersonDoesNotExist_ShouldReturnNoContent()
        {
            var client = _applicationFactory.CreateClient();

            var response = client.GetAsync("api/persons/0").Result;
            var actual = RequestHelper.ReadResponse<Person>(response);

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            Assert.Null(actual);
        }

        [Fact]
        public void Test_UpdateAddress()
        {
            var client = _applicationFactory.CreateClient();

            var repository = _applicationFactory.Server.Host.Services.GetService<IRepository<Person>>();
            var testPerson = new Person{Id = 123, Address = "old"};
            repository.Save(testPerson);

            var content = RequestHelper.CreateContent(new UpdateAddressViewModel { Address = "updated" });
            var updateResponse = client.PutAsync("api/Persons/123/UpdateAddress", content).Result;
            Assert.Equal(HttpStatusCode.OK, updateResponse.StatusCode);

            var actual = repository.Get(123);
            Assert.Equal("updated", actual.Address);
        }
    }
}
