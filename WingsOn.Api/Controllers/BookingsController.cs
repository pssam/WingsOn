using System.Net;
using Microsoft.AspNetCore.Mvc;
using WingsOn.Api.BusinessLogic.CommandHandlers;
using WingsOn.Api.ExceptionHandling;
using WingsOn.Api.ViewModels;

namespace WingsOn.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private static readonly object _lockObject = new object();
        private readonly IAddPassengerCommandHandler _addPassengerCommandHandler;

        public BookingsController(IAddPassengerCommandHandler addPassengerCommandHandler)
        {
            _addPassengerCommandHandler = addPassengerCommandHandler;
        }

        /// <summary>
        /// Creates a booking of an existing flight for a new passenger.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ClientError), (int)HttpStatusCode.BadRequest)]
        public ActionResult AddPassenger([FromBody] AddPassengerViewModel args)
        {
            lock (_lockObject)
            {
                _addPassengerCommandHandler.Handle(args.FlightNumber, args.CustomerId, args.PassengerId);
            }

            return Ok();
        }
    }
}