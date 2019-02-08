using System;
using System.Linq;
using WingsOn.Dal;
using WingsOn.Domain;

namespace WingsOn.Api.BusinessLogic.Factories
{
    public class BookingFactory : IBookingFactory
    {
        private readonly IRepository<Booking> _bookingRepository;

        public BookingFactory(IRepository<Booking> bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public Booking Create()
        {
            return new Booking
            {
                Id = GetNextId(),
                Number = GetNextNumber(),
                DateBooking = DateTime.UtcNow,
            };
        }

        private string GetNextNumber()
        {
            string maxNumber = "WO-000000";
            var numbers = _bookingRepository.GetAll().Select(x => x.Number);
            if (numbers.Any())
            {
                maxNumber = numbers.Max();
            }

            var numberId = int.Parse(maxNumber.Split("-")[1]) + 1;
            return "WO-" + numberId.ToString("D6");
        }

        private int GetNextId()
        {
            int maxId = 0;
            var ids = _bookingRepository.GetAll().Select(x => x.Id);
            if (ids.Any())
            {
                maxId = ids.Max();
            }

            return maxId + 1;
        }
    }
}