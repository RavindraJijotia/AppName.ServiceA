using AppName.ServiceA.Messages;

namespace AppName.ServiceA.Services.Interfaces
{
    public interface IMessageService
    {
        void SendMessage(INameMessage nameMessage);
    }
}
