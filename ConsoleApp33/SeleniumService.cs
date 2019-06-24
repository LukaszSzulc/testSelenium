using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Chrome;
using Microsoft.Extensions.Logging;

namespace ConsoleApp33
{

    public class SeleniumService : ISeleniumService
    {
        private readonly IOptions<SeleniumConfiguration> options;
        private readonly ILogger<SeleniumService> logger;

        public SeleniumService(IOptions<SeleniumConfiguration> options, ILogger<SeleniumService> logger)
        {
            this.options = options;
            this.logger = logger;
        }

        public Task RunAsync()
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--headless");
            using (var driver = new RemoteWebDriver(new Uri(options.Value.Url), chromeOptions))
            {
                driver.Navigate().GoToUrl("https://google.com");
                logger.LogInformation(driver.Title);
            }

            return Task.CompletedTask;
        }
    }
}
