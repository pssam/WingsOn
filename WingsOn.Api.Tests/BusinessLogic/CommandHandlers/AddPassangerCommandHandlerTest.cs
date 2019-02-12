using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WingsOn.Api.ExceptionHandling;
using WingsOn.Domain;

namespace WingsOn.Api.Tests.BusinessLogic.CommandHandlers
{
    [TestClass]
    public class AddPassengerCommandHandlerTest
    {
        [TestMethod]
        public void Handle_WhenFlightDoesNotExits_ShouldThrowException()
        {
            var builder = new AddPassengerCommandHandlerBuilder().WithPerson(1);
            var handler = builder.Build();

            Assert.ThrowsException<ValidationException>(() => handler.Handle("1", 1, 1));
        }

        [TestMethod]
        public void Handle_WhenCustomerDoesNotExits_ShouldThrowException()
        {
            var builder = new AddPassengerCommandHandlerBuilder().WithFlight("1").WithPerson(1);
            var handler = builder.Build();

            Assert.ThrowsException<ValidationException>(() => handler.Handle("1", 2, 1));
        }

        [TestMethod]
        public void Handle_WhenPassengerDoesNotExits_ShouldThrowException()
        {
            var builder = new AddPassengerCommandHandlerBuilder().WithFlight("1").WithPerson(1);
            var handler = builder.Build();

            Assert.ThrowsException<ValidationException>(() => handler.Handle("1", 1, 2));
        }

        [TestMethod]
        public void Handle_WhenPassengerIsAlreadyRegistered_ShouldThrowException()
        {
            var builder = new AddPassengerCommandHandlerBuilder().WithFlight("1").WithPerson(1).WithPassenger("1", 1);
            var handler = builder.Build();

            Assert.ThrowsException<ValidationException>(() => handler.Handle("1", 1, 1));
        }

        [TestMethod]
        public void Handle_WhenBookingIsValid_ShouldAddNewBooking()
        {
            var builder = new AddPassengerCommandHandlerBuilder().WithFlight("1").WithPerson(1);
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