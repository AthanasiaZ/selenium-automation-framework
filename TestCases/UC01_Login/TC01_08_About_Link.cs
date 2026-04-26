using OpenQA.Selenium;
using PageObjects.Inventory;
using PageObjects.Login;
using SeleniumAutomation.Core;

namespace TestCases.UC01_Global_Navigation
{
    internal class TC01_08_About_Link : WebDriverBase
    {
        public TC01_08_About_Link(IWebDriver driver) : base(driver) { }

        internal TC01_08_About_Link Check()
        {
            var login = new Login(Driver);
            var inventory = new Inventory(Driver);

            login.Open();
            login.LoginAs(
                TestConfig.StandardUser.Username,
                TestConfig.StandardUser.Password
            );

            Assert.That(inventory.IsOnInventoryPage(), Is.True,
                "Inventory page did not load.");

            var originalWindow = Driver.CurrentWindowHandle;
            var existingWindows = Driver.WindowHandles;

            WaitUntilClick(() => inventory.Locators.BurgerMenuButton());

            Wait.Until(driver =>
                driver.FindElements(By.Id("about_sidebar_link")).Any()
            );

            WaitUntilClick(() => inventory.Locators.AboutLink());

            Wait.Until(driver =>
                driver.WindowHandles.Count > existingWindows.Count
                || driver.Url.Contains("saucelabs")
            );

            if (Driver.WindowHandles.Count > existingWindows.Count)
            {
                var newWindow = Driver.WindowHandles.Last();
                Driver.SwitchTo().Window(newWindow);
            }

            Assert.That(Driver.Url.ToLower(), Does.Contain("saucelabs"),
                "About link did not redirect to Sauce Labs site.");

            return this;
        }
    }
}