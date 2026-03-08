using OpenQA.Selenium;
using SeleniumAutomation.Core;
using SeleniumAutomation.WebComponents.Login;

namespace SeleniumAutomation.TestCases.UC_Login
{
    internal class TC_Login_EmptyCredentials : WebDriverBase
    {
        public TC_Login_EmptyCredentials(IWebDriver driver) : base(driver) { }

        internal TC_Login_EmptyCredentials Check()
        {
            var login = new Login(Driver);

            login.Open();
            login.ClickLogin();

            Assert.That(login.GetErrorMessage(), Does.Contain("Username is required"));
            return this;
        }
    }
}