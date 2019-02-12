using System.Collections.Generic;
using System.Linq;
using WingsOn.Api.Helpers;
using WingsOn.Dal;
using WingsOn.Domain;

namespace WingsOn.Api.BusinessLogic.QueryHandlers
{
    public class GetPassengersQueryHandler
        : IGetPassengersQueryHandler
    {
        private readonly IRepository<Booking> _bookingRepository;

        public GetPassengersQueryHandler(IRepository<Booking> bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public IEnumerable<Person> Handle(string flightNumber, GenderType? gender)
        {
            var bookings = LoadBookings(flightNumber);
            var passengers = SelectPassengers(bookings, gender);
            return passengers;
        }

        private static IEnumerable<Person> SelectPassengers(IEnumerable<Booking> bookings, GenderType? gender)
        {
            var passengers = bookings.SelectMany(x => x.Passengers).Distinct(x => x.Id);
            if (gender != null)
            {
                passengers = passengers.Where(x => x.Gender == gender);
            }

            return passengers;
        }

        private IEnumerable<Booking> LoadBookings(string flightNumber)
        {
            var bookings = _bookingRepository.GetAll();
            if (!string.IsNullOrEmpty(flightNumber))
            {
                bookings = bookings.Where(x => x.Flight.Number == flightNumber);
            }

            return bookings;
        }
    }
}