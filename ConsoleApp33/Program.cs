using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ConsoleApp33
{
    class Program
    {
        private readonly ILogger<Program> logger;

        public Program(ILogger<Program> logger)
        {
            this.logger = logger;
        }
        static async Task Main(string[] args)
        {
            var services = ConfigureServices.Create();
            var application = services.GetRequiredService<Program>();
            await application.RunAsync();
        }

        public Task RunAsync()
        {
            logger.LogInformation("Hello");
            logger.LogInformation("Test");
            logger.LogInformation("Finished");
            return Task.CompletedTask;
        }
    }
}
