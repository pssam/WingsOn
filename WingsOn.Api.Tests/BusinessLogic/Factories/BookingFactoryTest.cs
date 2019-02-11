using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WingsOn.Api.BusinessLogic.Factories;
using WingsOn.Dal;
using WingsOn.Domain;

namespace WingsOn.Api.Tests.BusinessLogic.Factories
{
    [TestClass]
    public class BookingFactoryTest
    {
        [TestMethod]
        public void Test_Create_GeneratesValidIds()
        {
            var repository = new Mock<IRepository<Booking>>();
            SetupBooking(repository, 1, "WO-000001");
            var factory = new BookingFactory(repository.Object);

            var actual = factory.Create();

            Assert.AreEqual(2, actual.Id);
            Assert.AreEqual("WO-000002", actual.Number);
        }

        private static void SetupBooking(Mock<IRepository<Booking>> repository, int id, string number)
        {
            repository.Setup(x => x.GetAll()).Returns(new[]
            {
                new Booking
                {
                    Id = id,
                    Number = number,
                }
            });
        }
    }
}
