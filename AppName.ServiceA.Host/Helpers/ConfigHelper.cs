using Microsoft.Extensions.Configuration;
using System.IO;

namespace AppName.ServiceA.Host.Helpers
{
    public static class ConfigHelper
    {
        public static IConfigurationRoot Configuration { get; private set; }
        public static void LoadConfig(string[] args)
        {
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "Configuration"))
                .AddJsonFile("settings.json", optional: true, reloadOnChange: true);
            
            Configuration = configBuilder.Build();
        }
    }
}
