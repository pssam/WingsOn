using System.Collections.Generic;
using System.Linq;
using WingsOn.Api.Helpers;
using WingsOn.Dal;
using WingsOn.Domain;

namespace WingsOn.Api.BusinessLogic.QueryHandlers
{
    public class GetPassangersQueryHandler
        : IGetPassangersQueryHandler
    {
        private readonly IRepository<Booking> _bookingRepository;

        public GetPassangersQueryHandler(IRepository<Booking> bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public IEnumerable<Person> Handle(string flightNumber, GenderType? gender)
        {
            var bookings = LoadBookings(flightNumber);
            var passangers = SelectPassangers(bookings, gender);
            return passangers;
        }

        private static IEnumerable<Person> SelectPassangers(IEnumerable<Booking> bookings, GenderType? gender)
        {
            var passangers = bookings.SelectMany(x => x.Passengers).Distinct(x => x.Id);
            if (gender != null)
            {
                passangers = passangers.Where(x => x.Gender == gender);
            }

            return passangers;
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