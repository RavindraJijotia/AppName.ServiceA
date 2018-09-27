using System;
using System.Collections.Generic;
using System.Text;
using AppName.ServiceA.Models;
using AppName.ServiceA.Models.Configurations;
using AppName.ServiceA.Services.Implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace AppName.ServiceA.UnitTests
{
    [TestClass]
    [TestCategory("RabbitMqServiceTests")]
    public class RabbitMqServiceTests
    {
        private readonly Mock<RabbitMqOptions> _rabbitMqOptionsMock = new Mock<RabbitMqOptions>();
        private readonly Mock<IModel> _modelMock = new Mock<IModel>();
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RabbitMqService_PublishMessage_When_Null_Throws_Exception()
        {
            string message = string.Empty;
            var rabbitMqService = new RabbitMqService(_rabbitMqOptionsMock.Object);
            rabbitMqService.PublishMessage(message);
        }

        [TestMethod]
        public void RabbitMqService_PublishMessage_When_Have_Valid_Message_Sends_To_RabbitMq()
        {
            _rabbitMqOptionsMock.Object.HostName = "localhost";
            _rabbitMqOptionsMock.Object.NameMessageExchangeName = "AppName.Service.NameMessageExchange";
            _rabbitMqOptionsMock.Object.NameMessageQueueName = "AppName.Service.NameMessageQueue";
            _rabbitMqOptionsMock.Object.JsonMimeType = "application/json";

            var objNameMessage = new NameMessage
            {
                Message = "Name"
            };

            var serializedMessage = JsonConvert.SerializeObject(objNameMessage);

            var rabbitMqService = new RabbitMqService(_rabbitMqOptionsMock.Object);
            rabbitMqService.CreateConnection();
            rabbitMqService.PublishMessage(serializedMessage);
        }
    }
}
