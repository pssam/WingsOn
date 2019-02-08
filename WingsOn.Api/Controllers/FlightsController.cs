using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WingsOn.Dal;
using WingsOn.Domain;

namespace WingsOn.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly IRepository<Booking> _bookingRepository;

        public FlightsController(IRepository<Booking> bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        [HttpGet]
        [Route("{number}/passangers")]
        public ActionResult<IEnumerable<Person>> GetPassangers(string number)
        {
            var bookings = _bookingRepository.GetAll();
            if (number != null)
            {
                bookings = bookings.Where(x => x.Flight.Number == number);
            }

            var passangers = bookings.SelectMany(x => x.Passengers);

            return Ok(passangers);
        }
    }
}