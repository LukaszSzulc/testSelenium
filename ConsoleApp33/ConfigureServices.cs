using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace ConsoleApp33
{

    public static class ConfigureServices
    {
        public static ServiceProvider Create()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false).Build();
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddOptions();
            serviceCollection.AddLogging(logger =>
            {
                logger.AddConsole();
            });

            serviceCollection.AddTransient<Program>();
            return serviceCollection.BuildServiceProvider();
        }
    }
}
