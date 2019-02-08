using System.Net;
using Microsoft.AspNetCore.Mvc;
using WingsOn.Api.BusinessLogic.CommandHandlers;
using WingsOn.Api.ExceptionHandling;

namespace WingsOn.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private static readonly object LockObject = new object();
        private readonly IAddPassangerCommandHandler _addPassangerCommandHandler;

        public BookingsController(IAddPassangerCommandHandler addPassangerCommandHandler)
        {
            _addPassangerCommandHandler = addPassangerCommandHandler;
        }

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ClientError), (int)HttpStatusCode.BadRequest)]
        public ActionResult AddPassanger(string flightNumber, int customerId, int passangerId)
        {
            lock (LockObject)
            {
                _addPassangerCommandHandler.Handle(flightNumber, customerId, passangerId);
            }

            return Ok();
        }
    }
}