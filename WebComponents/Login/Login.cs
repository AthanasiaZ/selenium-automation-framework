using OpenQA.Selenium;
using SeleniumAutomation.Core;

namespace SeleniumAutomation.WebComponents.Login
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

            public IWebElement InventoryContainer(string condition = "find")
            {
                return GetElementByCss("#inventory_container", condition);
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
            WaitUntilClick(() => Locators.Username("visible"));
            Locators.Username().Clear();
            Locators.Username().SendKeys(username);
        }

        public void EnterPassword(string password)
        {
            WaitUntilClick(() => Locators.Password("visible"));
            Locators.Password().Clear();
            Locators.Password().SendKeys(password);
        }

        public void ClickLogin()
        {
            WaitUntilClick(() => Locators.LoginButton("clickable"));
        }

        public void LoginAs(string username, string password)
        {
            EnterUsername(username);
            EnterPassword(password);
            ClickLogin();
        }

        public string GetErrorMessage()
        {
            return Locators.ErrorMessage("visible").Text;
        }

        public bool IsOnInventoryPage()
        {
            return IsElementPresentByCss("#inventory_container");
        }
    }
}