using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WingsOn.Api.BusinessLogic.QueryHandlers;
using WingsOn.Domain;

namespace WingsOn.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassengersController : ControllerBase
    {
        private readonly IGetPassengersQueryHandler _getPassengersQueryHandler;

        public PassengersController(IGetPassengersQueryHandler getPassengersQueryHandler)
        {
            _getPassengersQueryHandler = getPassengersQueryHandler;
        }

        /// <summary>
        /// Returns all passengers.
        /// </summary>
        /// <param name="flightNumber">Filters passengers by flight (example: 'PZ696')</param>
        /// <param name="gender">Filters passengers by gender (example: 0)</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<Person>> Get(string flightNumber, GenderType? gender)
        {
            var passengers = _getPassengersQueryHandler.Handle(flightNumber, gender);

            return Ok(passengers);
        }
    }
}