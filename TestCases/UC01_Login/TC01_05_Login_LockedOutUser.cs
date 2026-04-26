using OpenQA.Selenium;
using SeleniumAutomation.Core;
using PageObjects.Login;

namespace TestCases.UC01_Global_Navigation
{
    internal class TC01_05_Login_LockedOutUser : WebDriverBase
    {
        public TC01_05_Login_LockedOutUser(IWebDriver driver) : base(driver) { }

        internal TC01_05_Login_LockedOutUser Check()
        {
            var login = new Login(Driver);
            login.Open();
            login.LoginAs(TestConfig.LockedOutUser.Username, TestConfig.LockedOutUser.Password);
            Assert.That(login.GetErrorMessage(), Does.Contain("Sorry, this user has been locked out."));
            return this;
        }
    }
}