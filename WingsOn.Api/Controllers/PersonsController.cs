using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
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
    }
}