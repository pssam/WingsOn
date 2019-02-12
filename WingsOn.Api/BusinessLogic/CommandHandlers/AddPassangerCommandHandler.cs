using System.Linq;
using WingsOn.Api.BusinessLogic.Factories;
using WingsOn.Api.ExceptionHandling;
using WingsOn.Dal;
using WingsOn.Domain;

namespace WingsOn.Api.BusinessLogic.CommandHandlers
{
    public class AddPassengerCommandHandler: IAddPassengerCommandHandler
    {
        private readonly IRepository<Booking> _bookingRepository;
        private readonly IRepository<Flight> _flightRepository;
        private readonly IRepository<Person> _personRepository;
        private readonly IBookingFactory _bookingFactory;

        public AddPassengerCommandHandler(IRepository<Booking> bookingRepository, IRepository<Flight> flightRepository,
            IRepository<Person> personRepository, IBookingFactory bookingFactory)
        {
            _bookingRepository = bookingRepository;
            _flightRepository = flightRepository;
            _personRepository = personRepository;
            _bookingFactory = bookingFactory;
        }

        public void Handle(string flightNumber, int customerId, int passengerId)
        {
            var flight = _flightRepository.GetAll().SingleOrDefault(x => x.Number == flightNumber);
            if (flight == null)
            {
                throw new ValidationException("Flight doesn not exist");
            }

            var customer = _personRepository.Get(customerId);
            if (customer == null)
            {
                throw new ValidationException("Customer doesn not exist");
            }

            var passenger = _personRepository.Get(passengerId);
            if (passenger == null)
            {
                throw new ValidationException("Passenger doesn not exist");
            }

            var existingPassengerBooking = FindBooking(flightNumber, passengerId);
            if (existingPassengerBooking != null)
            {
                throw new ValidationException("The Passenger is already registered for the flight");
            }

            var newBooking = _bookingFactory.Create();
            newBooking.Customer = customer;
            newBooking.Flight = flight;
            newBooking.Passengers = new[] { passenger };
            _bookingRepository.Save(newBooking);
        }

        private Booking FindBooking(string flightNumber, int passengerId)
        {
            var existingPassengerBooking = _bookingRepository.GetAll().FirstOrDefault(x =>
                x.Flight.Number == flightNumber &&
                x.Passengers.Any(flightPassenger =>
                    flightPassenger.Id == passengerId));
            return existingPassengerBooking;
        }
    }
}
