using OpenQA.Selenium;
using OpenQA.Selenium.BiDi.BrowsingContext;
using PageObjects.Login;
using SeleniumAutomation.Core;

namespace TestCases.UC01_Global_Navigation
{
    internal class TC01_03_Login_EmptyCredentials : WebDriverBase
    {
        public TC01_03_Login_EmptyCredentials(IWebDriver driver) : base(driver) { }

        internal TC01_03_Login_EmptyCredentials Check()
        {
            var login = new Login(Driver);

            login.Open();
            WaitUntilClick(() => login.Locators.LoginButton("clickable"));
            Assert.That(login.GetErrorMessage(), Does.Contain("Username is required"));
            return this;
        }
    }
}