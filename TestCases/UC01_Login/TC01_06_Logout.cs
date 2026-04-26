using OpenQA.Selenium;
using SeleniumAutomation.Core;
using PageObjects.Login;
using PageObjects.Inventory;

namespace TestCases.UC01_Global_Navigation
{
    internal class TC01_06_Logout : WebDriverBase
    {
        public TC01_06_Logout(IWebDriver driver) : base(driver) { }

        internal TC01_06_Logout Check()
        {
            var login = new Login(Driver);
            var inventory = new Inventory(Driver);

            login.Open();
            login.LoginAs(
                TestConfig.StandardUser.Username,
                TestConfig.StandardUser.Password
            );

            Assert.That(inventory.IsOnInventoryPage(), Is.True,
                "Inventory page did not load after login.");

            inventory.Logout();

            Assert.That(login.IsOnLoginPage(), Is.True,
                "User was not redirected to login page after logout.");

            return this;
        }
    }
}