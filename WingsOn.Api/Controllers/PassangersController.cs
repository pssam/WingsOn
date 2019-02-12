using System.Collections.Generic;
using System.Linq;
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

        [HttpGet]
        public ActionResult<IEnumerable<Person>> Get(string flightNumber, GenderType? gender)
        {
            var passengers = _getPassengersQueryHandler.Handle(flightNumber, gender);

            return Ok(passengers);
        }
    }
}