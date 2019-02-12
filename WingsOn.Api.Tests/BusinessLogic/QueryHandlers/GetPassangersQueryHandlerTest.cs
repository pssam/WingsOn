using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WingsOn.Api.BusinessLogic.QueryHandlers;
using WingsOn.Dal;
using WingsOn.Domain;

namespace WingsOn.Api.Tests.BusinessLogic.QueryHandlers
{
    [TestClass]
    public class GetPassengersQueryHandlerTest
    {
        [TestMethod]
        public void Handle_WhenFlightPassed_ShoudFilterPassengers()
        {
            Mock<IRepository<Booking>> repository = new Mock<IRepository<Booking>>();
            var passenger1 = new Person { Id = 1 };
            var passenger2 = new Person { Id = 2 };
            repository.Setup(x => x.GetAll()).Returns(new[]
            {
                new Booking
                {
                    Flight = new Flight { Number = "tst" },
                    Passengers = new[] { passenger1 }
                },
                new Booking
                {
                    Flight = new Flight { Number = "miss" },
                    Passengers = new[] { passenger2 }
                },
            });
            var handler = new GetPassengersQueryHandler(repository.Object);

            var actual = handler.Handle("tst", null);

            actual.Should().BeEquivalentTo(passenger1);
        }

        [TestMethod]
        public void Handle_WhenNoFilters_ShoudReturnAllPassengersWithoutDplication()
        {
            Mock<IRepository<Booking>> repository = new Mock<IRepository<Booking>>();
            var passenger = new Person { Id = 1 };
            repository.Setup(x => x.GetAll()).Returns(new[]
            {
                new Booking
                {
                    Flight = new Flight { Number = "f1" },
                    Passengers = new[] { passenger }
                },
                new Booking
                {
                    Flight = new Flight { Number = "f2" },
                    Passengers = new[] { passenger }
                },
            });
            var handler = new GetPassengersQueryHandler(repository.Object);

            var actual = handler.Handle(null, null);

            actual.Should().BeEquivalentTo(passenger);
        }

        [TestMethod]
        public void Handle_WhenGenderPassed_ShoudFilterByGender()
        {
            Mock<IRepository<Booking>> repository = new Mock<IRepository<Booking>>();
            var malePassenger = new Person { Id = 1, Gender = GenderType.Male};
            var femalePassenger = new Person { Id = 2, Gender = GenderType.Female};
            repository.Setup(x => x.GetAll()).Returns(new[]
            {
                new Booking
                {
                    Flight = new Flight { Number = "f1" },
                    Passengers = new[] { malePassenger }
                },
                new Booking
                {
                    Flight = new Flight { Number = "f2" },
                    Passengers = new[] { femalePassenger }
                },
            });
            var handler = new GetPassengersQueryHandler(repository.Object);

            var actual = handler.Handle(null, GenderType.Male);

            actual.Should().BeEquivalentTo(malePassenger);
        }
    }
}