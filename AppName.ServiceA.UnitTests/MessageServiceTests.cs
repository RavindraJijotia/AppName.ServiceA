using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AppName.ServiceA.Models;
using AppName.ServiceA.Services.Implementations;
using AppName.ServiceA.Services.Interfaces;
using Castle.Core.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AppName.ServiceA.UnitTests
{
    [TestClass]
    [TestCategory("MessageServiceTests")]
    public class MessageServiceTests
    {
        private readonly Mock<IRabbitMqService> _rabbitMqServiceMock = new Mock<IRabbitMqService>();

        [TestInitialize]
        public void TestFixtureSetup()
        {
            _rabbitMqServiceMock.Setup(s => s.CreateConnection());
            
            _rabbitMqServiceMock.Setup(s => s.CloseConnection());
        }


        [TestMethod]
        public void MessageService_PublishMessage_When_Have_Valid_Input_Executes_Successfully()
        {
            var nameMessage = new NameMessage
            {
                Message = "Valid"
            };

            _rabbitMqServiceMock.Setup(s => s.PublishMessage(It.IsAny<string>()));

            var messageService = new MessageService(_rabbitMqServiceMock.Object);
            messageService.SendMessage(nameMessage);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MessageService_PublishMessage_When_Have_Invalid_Input_Throws_Exception()
        {
            NameMessage nameMessage = null;

            _rabbitMqServiceMock.Setup(s => s.PublishMessage(It.IsAny<string>()));

            var messageService = new MessageService(_rabbitMqServiceMock.Object);
            messageService.SendMessage(nameMessage);
        }
    }
}
