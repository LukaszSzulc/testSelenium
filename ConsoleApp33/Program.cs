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
        private readonly ISeleniumService seleniumService;

        public Program(ILogger<Program> logger, ISeleniumService seleniumService)
        {
            this.logger = logger;
            this.seleniumService = seleniumService;
        }
        static async Task Main(string[] args)
        {
            var services = ConfigureServices.Create();
            var application = services.GetRequiredService<Program>();
            await application.RunAsync();
        }

        public async Task RunAsync()
        {
            try
            {
                logger.LogInformation("Hello");
                logger.LogInformation("Test");
                logger.LogInformation("Finished");
                await seleniumService.RunAsync();

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Selenium Error");
            }
        }
    }
}
