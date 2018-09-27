namespace AppName.ServiceA.Models.Configurations
{
    public class RabbitMqOptions
    {
        public string HostName { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string JsonMimeType { get; set; }
        public string NameMessageExchangeName { get; set; }
        public string NameMessageQueueName { get; set; }
    }
}
