using OpenQA.Selenium;
using SeleniumAutomation.Core;
using PageObjects.Inventory;
using PageObjects.Login;

namespace TestCases.UC01_Global_Navigation
{
    internal class TC01_01_Login_ValidUser : WebDriverBase
    {
        public TC01_01_Login_ValidUser(IWebDriver driver) : base(driver) { }

        internal TC01_01_Login_ValidUser Check()
        {
            var login = new Login(Driver);
            var inventory = new Inventory(Driver);
            login.Open();
            login.LoginAs(TestConfig.StandardUser.Username, TestConfig.DefaultPassword);
            Assert.That(inventory.IsOnInventoryPage(), Is.True);
            Assert.That(inventory.HasProducts(), Is.True);
            Assert.That(inventory.GetProductCount(), Is.GreaterThan(0));
            return this;
        }
    }
}