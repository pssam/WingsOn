namespace WingsOn.Api.BusinessLogic.CommandHandlers
{
    public interface IUpdatePersonAddressCommandHandler
    {
        void Handle(int id, string newAddress);
    }
}