﻿using Microsoft.Extensions.Configuration;
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
            serviceCollection.Configure<SeleniumConfiguration>(config.GetSection("Selenium"));
            serviceCollection.Configure<AzureConfiguration>(config.GetSection("AzureStorage"));
            serviceCollection.AddOptions();
            serviceCollection.AddLogging(logger =>
            {
                logger.AddConsole();
            });

            serviceCollection.AddTransient<ISeleniumService, SeleniumService>();
            serviceCollection.AddTransient<IDomainProvider, DomainProvider>();
            serviceCollection.AddTransient<IAzureStorage, AzureStorage>();
            serviceCollection.AddTransient<Program>();
            return serviceCollection.BuildServiceProvider();
        }
    }
}
