using OpenQA.Selenium;
using SeleniumAutomation.Core;

namespace PageObjects.Login
{
    public class Login : WebDriverBase
    {
        public LoginLocators Locators
        {
            get { return new LoginLocators(Driver); }
        }

        public Login(IWebDriver driver) : base(driver) { }

        public class LoginLocators : WebDriverBase
        {
            public LoginLocators(IWebDriver driver) : base(driver) { }

            public IWebElement Username(string condition = "find")
            {
                return GetElementByCss("#user-name", condition);
            }

            public IWebElement Password(string condition = "find")
            {
                return GetElementByCss("#password", condition);
            }

            public IWebElement LoginButton(string condition = "find")
            {
                return GetElementByCss("#login-button", condition);
            }

            public IWebElement ErrorMessage(string condition = "find")
            {
                return GetElementByCss("[data-test='error']", condition);
            }

            public IWebElement LoginLogo(string condition = "find")
            {
                return GetElementByCss(".login_logo", condition);
            }
        }

        public void Open()
        {
            Driver.Navigate().GoToUrl(TestConfig.BaseUrl);
            Assert.That(Locators.LoginLogo("visible").Displayed, Is.True, "Login Page is not loaded.");
        }

        public void EnterUsername(string username)
        {
            Locators.Username().Clear();
            Locators.Username().SendKeys(username);
        }

        public void EnterPassword(string password)
        {
            Locators.Password().Clear();
            Locators.Password().SendKeys(password);
        }

        public void LoginAs(string username, string password)
        {
            EnterUsername(username);
            EnterPassword(password);
            WaitUntilClick(() => Locators.LoginButton("clickable"));
        }

        public string GetErrorMessage()
        {
            return Locators.ErrorMessage("visible").Text;
        }

        public bool IsOnLoginPage()
        {
            return Driver.FindElements(By.Id("login-button")).Any();
        }

    }
}