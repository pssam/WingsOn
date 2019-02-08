using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using WingsOn.Api.ExceptionHandling;
using WingsOn.Dal;
using WingsOn.Domain;

namespace WingsOn.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IRepository<Person> _personRepository;

        public PersonsController(IRepository<Person> personRepository)
        {
            _personRepository = personRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Person>> Get()
        {
            var result = _personRepository.GetAll();
            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<Person> GetPerson(int id)
        {
            var result = _personRepository.Get(id);
            return Ok(result);
        }

        [HttpPut]
        [Route("{id}/[action]")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ClientError), (int)HttpStatusCode.BadRequest)]
        public ActionResult UpdateAddress(int id, string newAddress)
        {
            var person = _personRepository.Get(id);
            if (person == null)
            {
                throw new ValidationException("Person does not exist");
            }

            person.Address = newAddress;
            _personRepository.Save(person);

            return Ok();
        }
    }
}