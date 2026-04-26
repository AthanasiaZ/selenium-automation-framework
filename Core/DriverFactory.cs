using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumAutomation.Core
{
    public static class DriverFactory
    {
        public static IWebDriver CreateDriver()
        {
            var options = new ChromeOptions();

            options.AddArgument("--disable-save-password-bubble");
            options.AddArgument("--disable-features=PasswordLeakDetection");
            options.AddUserProfilePreference("credentials_enable_service", false);
            options.AddUserProfilePreference("profile.password_manager_enabled", false);
            options.AddUserProfilePreference("profile.password_manager_leak_detection", false);

            return new ChromeDriver(options);
        }
    }
}