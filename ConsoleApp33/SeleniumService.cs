using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Chrome;

namespace ConsoleApp33
{

    public class SeleniumService : ISeleniumService
    {
        private readonly IOptions<SeleniumConfiguration> options;

        public SeleniumService(IOptions<SeleniumConfiguration> options)
        {
            this.options = options;
        }

        public Task RunAsync()
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--headless");
            using (var driver = new RemoteWebDriver(new Uri(options.Value.Url), chromeOptions))
            {
            }

            return Task.CompletedTask;
        }
    }
}
