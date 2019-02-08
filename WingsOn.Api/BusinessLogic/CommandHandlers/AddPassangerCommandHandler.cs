using System.Linq;
using WingsOn.Api.BusinessLogic.Factories;
using WingsOn.Api.ExceptionHandling;
using WingsOn.Dal;
using WingsOn.Domain;

namespace WingsOn.Api.BusinessLogic.CommandHandlers
{
    public class AddPassangerCommandHandler: IAddPassangerCommandHandler
    {
        private readonly IRepository<Booking> _bookingRepository;
        private readonly IRepository<Flight> _flightRepository;
        private readonly IRepository<Person> _personRepository;
        private readonly IBookingFactory _bookingFactory;

        public AddPassangerCommandHandler(IRepository<Booking> bookingRepository, IRepository<Flight> flightRepository,
            IRepository<Person> personRepository, IBookingFactory bookingFactory)
        {
            _bookingRepository = bookingRepository;
            _flightRepository = flightRepository;
            _personRepository = personRepository;
            _bookingFactory = bookingFactory;
        }

        public void Handle(string flightNumber, int customerId, int passangerId)
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

            var passanger = _personRepository.Get(passangerId);
            if (customer == null)
            {
                throw new ValidationException("Passanger doesn not exist");
            }

            var existingPassangerBooking = FindBooking(flightNumber, passangerId);
            if (existingPassangerBooking != null)
            {
                throw new ValidationException("The Passanger is already registered for the flight");
            }

            var newBooking = _bookingFactory.Create();
            newBooking.Customer = customer;
            newBooking.Flight = flight;
            newBooking.Passengers = new[] { passanger };
            _bookingRepository.Save(newBooking);
        }

        private Booking FindBooking(string flightNumber, int passangerId)
        {
            var existingPassangerBooking = _bookingRepository.GetAll().FirstOrDefault(x =>
                x.Flight.Number == flightNumber &&
                x.Passengers.Any(flightPassanger =>
                    flightPassanger.Id == passangerId));
            return existingPassangerBooking;
        }
    }
}
