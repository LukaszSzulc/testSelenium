using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Chrome;

namespace ConsoleApp33
{

    public interface ISeleniumService
    {
        Task RunAsync();
    }
}
