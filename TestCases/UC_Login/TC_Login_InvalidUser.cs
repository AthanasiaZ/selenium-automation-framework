using OpenQA.Selenium;
using SeleniumAutomation.Core;
using SeleniumAutomation.WebComponents.Login;

namespace SeleniumAutomation.TestCases.UC_Login
{
    internal class TC_Login_InvalidUser : WebDriverBase
    {
        public TC_Login_InvalidUser(IWebDriver driver) : base(driver) { }

        internal TC_Login_InvalidUser Check()
        {
            var login = new Login(Driver);

            login.Open();
            login.LoginAs(TestConfig.InvalidUsername, TestConfig.InvalidPassword);
            Assert.That(login.GetErrorMessage(), Does.Contain("Username and password do not match"));
            return this;
        }
    }
}