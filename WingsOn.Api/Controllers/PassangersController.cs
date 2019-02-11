using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WingsOn.Api.BusinessLogic.QueryHandlers;
using WingsOn.Domain;

namespace WingsOn.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassangersController : ControllerBase
    {
        private readonly IGetPassangersQueryHandler _getPassangersQueryHandler;

        public PassangersController(IGetPassangersQueryHandler getPassangersQueryHandler)
        {
            _getPassangersQueryHandler = getPassangersQueryHandler;
        }

        [HttpGet]
        [Route("{gender}")]
        public ActionResult<IEnumerable<Person>> Get(string flightNumber, GenderType gender)
        {
            var passangers = _getPassangersQueryHandler.Handle(flightNumber, gender);

            return Ok(passangers);
        }
    }
}