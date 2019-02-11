using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WingsOn.Api.ExceptionHandling;
using WingsOn.Domain;

namespace WingsOn.Api.Tests.BusinessLogic.CommandHandlers
{
    [TestClass]
    public class AddPassangerCommandHandlerTest
    {
        [TestMethod]
        public void Handle_WhenFlightDoesNotExits_ShouldThrowException()
        {
            var builder = new AddPassangerCommandHandlerBuilder().WithPerson(1);
            var handler = builder.Build();

            Assert.ThrowsException<ValidationException>(() => handler.Handle("1", 1, 1));
        }

        [TestMethod]
        public void Handle_WhenCustomerDoesNotExits_ShouldThrowException()
        {
            var builder = new AddPassangerCommandHandlerBuilder().WithFlight("1").WithPerson(1);
            var handler = builder.Build();

            Assert.ThrowsException<ValidationException>(() => handler.Handle("1", 2, 1));
        }

        [TestMethod]
        public void Handle_WhenPassangerDoesNotExits_ShouldThrowException()
        {
            var builder = new AddPassangerCommandHandlerBuilder().WithFlight("1").WithPerson(1);
            var handler = builder.Build();

            Assert.ThrowsException<ValidationException>(() => handler.Handle("1", 1, 2));
        }

        [TestMethod]
        public void Handle_WhenPassangerIsAlreadyRegistered_ShouldThrowException()
        {
            var builder = new AddPassangerCommandHandlerBuilder().WithFlight("1").WithPerson(1).WithPassanger("1", 1);
            var handler = builder.Build();

            Assert.ThrowsException<ValidationException>(() => handler.Handle("1", 1, 1));
        }

        [TestMethod]
        public void Handle_WhenBookingIsValid_ShouldAddNewBooking()
        {
            var builder = new AddPassangerCommandHandlerBuilder().WithFlight("1").WithPerson(1);
            builder.BookingFactory.Setup(x => x.Create()).Returns(new Booking { Number = "tst", Id = 123 });
            var handler = builder.Build();

            handler.Handle("1", 1, 1);

            builder.BookingRepository.Verify(repository =>
                repository.Save(It.Is<Booking>(booking =>
                    booking.Number == "tst" && booking.Id == 123 && booking.Flight == builder.Flight &&
                    booking.Passengers.Single() == builder.Person)));
        }
    }
}