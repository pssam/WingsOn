using Moq;
using WingsOn.Api.BusinessLogic.CommandHandlers;
using WingsOn.Api.BusinessLogic.Factories;
using WingsOn.Dal;
using WingsOn.Domain;

namespace WingsOn.Api.Tests.BusinessLogic.CommandHandlers
{
    internal class AddPassengerCommandHandlerBuilder
    {
        public Mock<IRepository<Booking>> BookingRepository { get; set; } = new Mock<IRepository<Booking>>();

        public Mock<IRepository<Flight>> FlightRepository { get; set; } = new Mock<IRepository<Flight>>();

        public Mock<IRepository<Person>> PersonRepository { get; set; } = new Mock<IRepository<Person>>();

        public Mock<IBookingFactory> BookingFactory { get; set; } = new Mock<IBookingFactory>();

        public Flight Flight { get; private set; }

        public Person Person { get; private set; }

        public AddPassengerCommandHandler Build()
        {
            return new AddPassengerCommandHandler(BookingRepository.Object, FlightRepository.Object,
                PersonRepository.Object, BookingFactory.Object);
        }

        public AddPassengerCommandHandlerBuilder WithPerson(int personId)
        {
            Person = new Person { Id = personId };
            PersonRepository.Setup(x => x.Get(personId)).Returns(Person);
            return this;
        }

        public AddPassengerCommandHandlerBuilder WithFlight(string flightNumber)
        {
            Flight = new Flight { Number = flightNumber };
            FlightRepository.Setup(x => x.GetAll()).Returns(new[] { Flight, });
            return this;
        }

        public AddPassengerCommandHandlerBuilder WithPassenger(string flightNumber, int passengerId)
        {
            BookingRepository.Setup(x => x.GetAll()).Returns(new[]
            {
                new Booking
                {
                    Flight = new Flight { Number = flightNumber },
                    Passengers = new[] { new Person { Id = passengerId }, }
                },
            });
            return this;
        }
    }
}