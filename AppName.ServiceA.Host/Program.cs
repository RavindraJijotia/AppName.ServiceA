using AppName.ServiceA.Host.Helpers;
using AppName.ServiceA.Models;
using AppName.ServiceA.Models.Configurations;
using AppName.ServiceA.Services.Implementations;
using AppName.ServiceA.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.RegularExpressions;


namespace AppName.ServiceA.Host
{
    public class Program
    {
        public static IConfiguration Configuration { get; set; }
        public static ServiceProvider Container { get; set; }

        private const string ExitOption = "q";

        static void Main(string[] args)
        {
            try
            {
                Console.Title = "Publisher";

                //Register DI & load configuration from json file.
                var services = new ServiceCollection();
                ConfigHelper.LoadConfig(args);
                ConfigureServices(services);

                Console.WriteLine("Type q to Exit");

                var userInput = string.Empty;

                Container = services.BuildServiceProvider();

                do
                {
                    Console.Write("Enter name to send = ");

                    userInput = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(userInput) || !Regex.IsMatch(userInput, @"^[a-zA-Z]+$"))
                    {
                        Console.WriteLine("Invalid Name");
                        continue;
                    }

                    if (userInput != ExitOption)
                        PublishMessage($"Hello my name is, {userInput}");
                    else
                        Environment.Exit(0);

                } while (userInput != ExitOption);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(ConfigHelper.Configuration.GetSection("RabbitMQConfiguration")
                .Get<RabbitMqOptions>());

            services.AddTransient<IMessageService, MessageService>();
            services.AddTransient<IRabbitMqService, RabbitMqService>();
        }

        private static void PublishMessage(string name)
        {
            var messageService = Container.GetService<IMessageService>();
            messageService.SendMessage(new NameMessage { Message = name });
        }
    }
}

