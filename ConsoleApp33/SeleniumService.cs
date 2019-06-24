using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Chrome;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Collections.Concurrent;

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
            var list = new ConcurrentQueue<Task>();
            Parallel.ForEach(domains, parallelOptions: new ParallelOptions { MaxDegreeOfParallelism = 16 }, async (domain) =>
            {
                try
                {
                    using (var driver = new RemoteWebDriver(new Uri(options.Value.Url), chromeOptions))
                    {
                        driver.Navigate().GoToUrl($"http://{domain}");
                        var screenshoot = driver.GetScreenshot();
                        list.Enqueue(azureStorage.Put(screenshoot.AsByteArray, domain));
                        lock (lockObject)
                        {
                            logger.LogInformation($"processed {counter}/{domains.Count}");
                            Interlocked.Increment(ref counter);
                        }
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Selenium error");
                }
            });

            await Task.WhenAll(list);
        }
    }
}
