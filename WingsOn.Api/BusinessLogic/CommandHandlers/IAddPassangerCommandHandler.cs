namespace WingsOn.Api.BusinessLogic.CommandHandlers
{
    public interface IAddPassangerCommandHandler
    {
        void Handle(string flightNumber, int customerId, int passangerId);
    }
}