using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Chrome;
using Microsoft.Extensions.Logging;
using System.Threading;

namespace ConsoleApp33
{

    public class SeleniumService : ISeleniumService
    {
        private readonly IOptions<SeleniumConfiguration> options;
        private readonly ILogger<SeleniumService> logger;
        private readonly IDomainProvider domainServiceProvider;
        private readonly IAzureStorage azureStorage;

        public SeleniumService(IOptions<SeleniumConfiguration> options, ILogger<SeleniumService> logger, IDomainProvider domainServiceProvider, IAzureStorage azureStorage)
        {
            this.options = options;
            this.logger = logger;
            this.domainServiceProvider = domainServiceProvider;
            this.azureStorage = azureStorage;
        }

        public async Task RunAsync()
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--headless");
            var domains = await domainServiceProvider.Get();
            int counter = 1;
            object lockObject = new object();
            Parallel.ForEach(domains, parallelOptions: new ParallelOptions { MaxDegreeOfParallelism = 16 }, async (domain) =>
            {
                using (var driver = new RemoteWebDriver(new Uri(options.Value.Url), chromeOptions))
                {
                    driver.Navigate().GoToUrl($"http://{domain}");
                    var screenshoot = driver.GetScreenshot();
                    await azureStorage.Put(screenshoot.AsByteArray, domain);
                    lock (lockObject)
                    {
                        logger.LogInformation($"processed {counter}/{domains.Count}");
                        Interlocked.Increment(ref counter);
                    }
                }
            });
        }
    }
}
