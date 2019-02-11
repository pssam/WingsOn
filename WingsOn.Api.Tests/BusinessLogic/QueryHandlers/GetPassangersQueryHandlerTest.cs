using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WingsOn.Api.BusinessLogic.QueryHandlers;
using WingsOn.Dal;
using WingsOn.Domain;

namespace WingsOn.Api.Tests.BusinessLogic.QueryHandlers
{
    [TestClass]
    public class GetPassangersQueryHandlerTest
    {
        [TestMethod]
        public void Handle_WhenFlightPassed_ShoudFilterPassangers()
        {
            Mock<IRepository<Booking>> repository = new Mock<IRepository<Booking>>();
            var passanger1 = new Person { Id = 1 };
            var passanger2 = new Person { Id = 2 };
            repository.Setup(x => x.GetAll()).Returns(new[]
            {
                new Booking
                {
                    Flight = new Flight { Number = "tst" },
                    Passengers = new[] { passanger1 }
                },
                new Booking
                {
                    Flight = new Flight { Number = "miss" },
                    Passengers = new[] { passanger2 }
                },
            });
            var handler = new GetPassangersQueryHandler(repository.Object);

            var actual = handler.Handle("tst", null);

            actual.Should().BeEquivalentTo(passanger1);
        }

        [TestMethod]
        public void Handle_WhenNoFilters_ShoudReturnAllPassangersWithoutDplication()
        {
            Mock<IRepository<Booking>> repository = new Mock<IRepository<Booking>>();
            var passanger = new Person { Id = 1 };
            repository.Setup(x => x.GetAll()).Returns(new[]
            {
                new Booking
                {
                    Flight = new Flight { Number = "f1" },
                    Passengers = new[] { passanger }
                },
                new Booking
                {
                    Flight = new Flight { Number = "f2" },
                    Passengers = new[] { passanger }
                },
            });
            var handler = new GetPassangersQueryHandler(repository.Object);

            var actual = handler.Handle(null, null);

            actual.Should().BeEquivalentTo(passanger);
        }

        [TestMethod]
        public void Handle_WhenGenderPassed_ShoudFilterByGender()
        {
            Mock<IRepository<Booking>> repository = new Mock<IRepository<Booking>>();
            var malePassanger = new Person { Id = 1, Gender = GenderType.Male};
            var femalePassanger = new Person { Id = 2, Gender = GenderType.Female};
            repository.Setup(x => x.GetAll()).Returns(new[]
            {
                new Booking
                {
                    Flight = new Flight { Number = "f1" },
                    Passengers = new[] { malePassanger }
                },
                new Booking
                {
                    Flight = new Flight { Number = "f2" },
                    Passengers = new[] { femalePassanger }
                },
            });
            var handler = new GetPassangersQueryHandler(repository.Object);

            var actual = handler.Handle(null, GenderType.Male);

            actual.Should().BeEquivalentTo(malePassanger);
        }
    }
}