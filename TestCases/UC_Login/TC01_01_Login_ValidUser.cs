using OpenQA.Selenium;
using SeleniumAutomation.Core;
using SeleniumAutomation.WebComponents.Login;

namespace SeleniumAutomation.TestCases.UC01_Login
{
    internal class TC01_01_Login_ValidUser : WebDriverBase
    {
        public TC01_01_Login_ValidUser(IWebDriver driver) : base(driver) { }

        internal TC01_01_Login_ValidUser Check()
        {
            var login = new Login(Driver);

            login.Open();
            login.LoginAs(TestConfig.ValidUsername, TestConfig.ValidPassword);
            Assert.That(login.IsOnInventoryPage(), Is.True, "User was not redirected to the Inventory Page.");
            return this;
        }
    }
}