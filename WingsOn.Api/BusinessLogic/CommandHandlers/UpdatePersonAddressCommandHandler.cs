using WingsOn.Api.ExceptionHandling;
using WingsOn.Dal;
using WingsOn.Domain;

namespace WingsOn.Api.BusinessLogic.CommandHandlers
{
    public class UpdatePersonAddressCommandHandler
        : IUpdatePersonAddressCommandHandler
    {
        private readonly IRepository<Person> _personRepository;

        public UpdatePersonAddressCommandHandler(IRepository<Person> personRepository)
        {
            _personRepository = personRepository;
        }

        public void Handle(int id, string newAddress)
        {
            var person = _personRepository.Get(id);
            if (person == null)
            {
                throw new ValidationException("Person does not exist");
            }

            person.Address = newAddress;
            _personRepository.Save(person);
        }
    }
}