using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumAutomation.Core
{
    public static class DriverFactory
    {
        public static IWebDriver CreateDriver()
        {
            return new ChromeDriver();
        }
    }
}