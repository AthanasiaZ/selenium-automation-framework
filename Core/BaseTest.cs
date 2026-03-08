using OpenQA.Selenium;

namespace SeleniumAutomation.Core
{
    public class BaseTest
    {
        protected IWebDriver Driver;

        [SetUp]
        public void SetUp()
        {
            Driver = DriverFactory.CreateDriver();
            Driver.Manage().Window.Maximize();
        }

        [TearDown]
        public void TearDown()
        {
            if (Driver != null)
            {
                Driver.Dispose();
            }
        }
    }
}