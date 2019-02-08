using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WingsOn.Dal;
using WingsOn.Domain;

namespace WingsOn.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassangersController : ControllerBase
    {
        private readonly IRepository<Booking> _bookingRepository;

        public PassangersController(IRepository<Booking> bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        [HttpGet]
        [Route("{gender}")]
        public ActionResult<IEnumerable<Person>> Get(GenderType gender)
        {
            var passangers = _bookingRepository.GetAll().SelectMany(x => x.Passengers).Where(x => x.Gender == gender);
            return Ok(passangers);
        }
    }
}