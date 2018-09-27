namespace AppName.ServiceA.Services.Interfaces
{
    public interface IRabbitMqService
    {
        void CreateConnection();
        void PublishMessage(string message);
        void CloseConnection();
    }
}
