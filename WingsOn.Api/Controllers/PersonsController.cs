﻿using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using WingsOn.Api.BusinessLogic.CommandHandlers;
using WingsOn.Api.ExceptionHandling;
using WingsOn.Api.ViewModels;
using WingsOn.Dal;
using WingsOn.Domain;

namespace WingsOn.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IRepository<Person> _personRepository;
        private readonly IUpdatePersonAddressCommandHandler _updatePersonAddressCommandHandler;

        public PersonsController(IRepository<Person> personRepository, IUpdatePersonAddressCommandHandler updatePersonAddressCommandHandler)
        {
            _personRepository = personRepository;
            _updatePersonAddressCommandHandler = updatePersonAddressCommandHandler;
        }

        /// <summary>
        /// Returns a list of all persons.
        /// </summary>
        [HttpGet]
        public ActionResult<IEnumerable<Person>> Get()
        {
            var result = _personRepository.GetAll();
            return Ok(result);
        }

        /// <summary>
        /// Returns a person by Id.
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        public ActionResult<Person> GetPerson(int id)
        {
            var result = _personRepository.Get(id);
            return Ok(result);
        }

        /// <summary>
        /// Update person's Address.
        /// </summary>
        [HttpPut]
        [Route("{id}/[action]")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ClientError), (int)HttpStatusCode.BadRequest)]
        public ActionResult UpdateAddress(int id, [FromBody] UpdateAddressViewModel newAddress)
        {
            _updatePersonAddressCommandHandler.Handle(id, newAddress.Address);
            var result = Ok();
            return result;
        }
    }
}