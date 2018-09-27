using AppName.ServiceA.Models.Configurations;
using AppName.ServiceA.Services.Interfaces;
using RabbitMQ.Client;
using System;
using System.Text;

namespace AppName.ServiceA.Services.Implementations
{
    public class RabbitMqService : IRabbitMqService, IDisposable
    {
        private readonly RabbitMqOptions _rabbitMqOptions;
        private IModel _channel;

        public RabbitMqService(RabbitMqOptions rabbitMqOptions)
        {
            _rabbitMqOptions = rabbitMqOptions;
        }

        public void CreateConnection()
        {
            var connectionFactory =
                new ConnectionFactory { HostName = _rabbitMqOptions.HostName };
            var connection = connectionFactory.CreateConnection();
            _channel = connection.CreateModel();
        }

        public void PublishMessage(string serializedMessage)
        {
            if(string.IsNullOrWhiteSpace(serializedMessage)) throw new ArgumentNullException(nameof(serializedMessage));

            _channel.ExchangeDeclare(exchange: _rabbitMqOptions.NameMessageExchangeName, type: ExchangeType.Direct);
            _channel.QueueDeclare(queue: _rabbitMqOptions.NameMessageQueueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
            _channel.QueueBind(queue: _rabbitMqOptions.NameMessageQueueName, exchange: _rabbitMqOptions.NameMessageExchangeName, routingKey: "");

            var messageProperties = _channel.CreateBasicProperties();
            messageProperties.ContentType = _rabbitMqOptions.JsonMimeType;
            _channel.BasicPublish(exchange: _rabbitMqOptions.NameMessageExchangeName, routingKey: "", basicProperties: messageProperties, body: Encoding.UTF8.GetBytes(serializedMessage));
        }

        public void CloseConnection()
        {
            if (!_channel.IsClosed)
                _channel.Close();
        }

        public void Dispose()
        {
            if (!_channel.IsClosed)
                _channel.Close();
        }
        
    }
}
