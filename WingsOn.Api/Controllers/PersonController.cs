using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WingsOn.Dal;
using WingsOn.Domain;

namespace WingsOn.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IRepository<Person> _personRepository;

        public PersonController(IRepository<Person> personRepository)
        {
            _personRepository = personRepository;
        }

        public ActionResult<IEnumerable<Person>> GetPersons()
        {
            var result = _personRepository.GetAll();
            return Ok(result);
        }
    }
}