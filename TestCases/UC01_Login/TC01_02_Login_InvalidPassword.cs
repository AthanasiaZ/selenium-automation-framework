using OpenQA.Selenium;
using SeleniumAutomation.Core;
using PageObjects.Login;

namespace TestCases.UC01_Global_Navigation
{
    internal class TC01_02_Login_InvalidPassword : WebDriverBase
    {
        public TC01_02_Login_InvalidPassword(IWebDriver driver) : base(driver) { }

        internal TC01_02_Login_InvalidPassword Check()
        {
            var login = new Login(Driver);
            login.Open();
            login.LoginAs(TestConfig.StandardUser.Username, "wrong_password");
            Assert.That(login.GetErrorMessage(), Does.Contain("Username and password do not match"));
            return this;
        }
    }
}