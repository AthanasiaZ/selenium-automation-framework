using OpenQA.Selenium;
using SeleniumAutomation.Core;
using SeleniumAutomation.WebComponents.Login;

namespace SeleniumAutomation.TestCases.UC01_Login
{
    internal class TC01_04_Login_EmptyPassword : WebDriverBase
    {
        public TC01_04_Login_EmptyPassword(IWebDriver driver) : base(driver) { }

        internal TC01_04_Login_EmptyPassword Check()
        {
            var login = new Login(Driver);

            login.Open();
            login.LoginAs(TestConfig.InvalidUsername, "");
            Assert.That(login.GetErrorMessage(), Does.Contain("Password is required"));
            return this;
        }
    }
}