using System;
using System.Globalization;
using System.Linq;
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
    public class BookingsControllerTest : IClassFixture<TestApplicationFactory>
    {
        private readonly WebApplicationFactory<TestStartup> _applicationFactory;

        public BookingsControllerTest(TestApplicationFactory applicationFactory)
        {
            _applicationFactory = applicationFactory;
        }

        [Fact]
        public void AddPassanger_ShouldCreateBooking()
        {
            var client = _applicationFactory.CreateClient();

            var content = RequestHelper.CreateContent(new AddPassengerViewModel
                { CustomerId = 91, PassengerId = 91, FlightNumber = "BB768" });
            var response = client.PostAsync("api/bookings/addPassenger", content).Result;

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var repository = _applicationFactory.Server.Host.Services.GetService<IRepository<Booking>>();
            var booking = repository.GetAll().Single(x => x.Customer.Id == 91);
            var passenger = booking.Passengers.Single();
            Assert.NotNull(passenger);
        }
    }
}