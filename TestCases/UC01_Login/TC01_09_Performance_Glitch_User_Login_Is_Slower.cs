using OpenQA.Selenium;
using SeleniumAutomation.Core;
using PageObjects.Login;
using PageObjects.Inventory;
using System.Diagnostics;

namespace TestCases.UC01_Global_Navigation
{
    internal class TC01_09_Performance_Glitch_User_Login_Is_Slower : WebDriverBase
    {
        public TC01_09_Performance_Glitch_User_Login_Is_Slower(IWebDriver driver) : base(driver) { }

        internal TC01_09_Performance_Glitch_User_Login_Is_Slower Check()
        {
            var login = new Login(Driver);
            var inventory = new Inventory(Driver);

            login.Open();

            var standardTime = MeasureLoginTime(
                TestConfig.StandardUser.Username,
                TestConfig.StandardUser.Password,
                login,
                inventory
            );

            Driver.Manage().Cookies.DeleteAllCookies();

            login.Open();

            var glitchTime = MeasureLoginTime(
                TestConfig.PerformanceGlitchUser.Username,
                TestConfig.PerformanceGlitchUser.Password,
                login,
                inventory
            );

            Console.WriteLine($"Standard user login: {standardTime} ms");
            Console.WriteLine($"Performance glitch user login: {glitchTime} ms");

            Assert.That(glitchTime, Is.GreaterThan(standardTime),
                "Performance glitch user login was not slower than standard user login.");

            return this;
        }

        private long MeasureLoginTime(
            string username,
            string password,
            Login login,
            Inventory inventory)
        {
            var stopwatch = Stopwatch.StartNew();

            login.LoginAs(username, password);

            Wait.Until(_ => inventory.IsOnInventoryPage());

            stopwatch.Stop();

            return stopwatch.ElapsedMilliseconds;
        }
    }
}