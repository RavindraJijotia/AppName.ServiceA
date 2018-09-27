using AppName.ServiceA.Messages;
using AppName.ServiceA.Models;
using AppName.ServiceA.Services.Interfaces;
using Newtonsoft.Json;

namespace AppName.ServiceA.Services.Implementations
{
    public class MessageService : IMessageService
    {
        private readonly IRabbitMqService _rabbitMqService;

        public MessageService(IRabbitMqService rabbitMqService)
        {
            _rabbitMqService = rabbitMqService;
        }

        public void SendMessage(INameMessage nameMessage)
        {
            var objNameMessage = new NameMessage
            {
                Message = nameMessage.Message
            };

            var serializedMessage = JsonConvert.SerializeObject(objNameMessage);

            _rabbitMqService.CreateConnection();
            _rabbitMqService.PublishMessage(serializedMessage);
            _rabbitMqService.CloseConnection();
        }
    }
}
