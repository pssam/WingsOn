namespace WingsOn.Api.BusinessLogic.CommandHandlers
{
    public interface IAddPassengerCommandHandler
    {
        void Handle(string flightNumber, int customerId, int passengerId);
    }
}