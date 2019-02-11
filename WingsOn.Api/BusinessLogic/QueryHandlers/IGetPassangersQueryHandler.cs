using System.Collections.Generic;
using WingsOn.Domain;

namespace WingsOn.Api.BusinessLogic.QueryHandlers
{
    public interface IGetPassangersQueryHandler
    {
        IEnumerable<Person> Handle(string flightNumber, GenderType? gender);
    }
}