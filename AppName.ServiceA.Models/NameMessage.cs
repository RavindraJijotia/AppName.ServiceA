using AppName.ServiceA.Messages;

namespace AppName.ServiceA.Models
{
    public class NameMessage : INameMessage
    {
        public string Message { get; set; }
    }
}
