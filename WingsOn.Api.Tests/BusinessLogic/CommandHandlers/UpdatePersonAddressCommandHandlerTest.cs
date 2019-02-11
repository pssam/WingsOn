using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WingsOn.Api.BusinessLogic.CommandHandlers;
using WingsOn.Api.ExceptionHandling;
using WingsOn.Dal;
using WingsOn.Domain;

namespace WingsOn.Api.Tests.BusinessLogic.CommandHandlers
{
    [TestClass]
    public class UpdatePersonAddressCommandHandlerTest
    {
        [TestMethod]
        public void Handle_WhenPersonDoesNotExist_ShouldThrowException()
        {
            Mock<IRepository<Person>> repository = new Mock<IRepository<Person>>();
            var handler = new UpdatePersonAddressCommandHandler(repository.Object);

            Assert.ThrowsException<ValidationException>(() => handler.Handle(1, "newAddr"));
        }

        [TestMethod]
        public void Handle_WhenPersonExists_ShouldUpdateAddress()
        {
            Mock<IRepository<Person>> repository = new Mock<IRepository<Person>>();
            repository.Setup(x => x.Get(1)).Returns(new Person { Id = 1, Address = "old" });
            var handler = new UpdatePersonAddressCommandHandler(repository.Object);

            handler.Handle(1, "newAddr");

            repository.Verify(repositoryMock =>
                repositoryMock.Save(It.Is<Person>(person => person.Id == 1 && person.Address == "newAddr")));
        }
    }
}