using OpenQA.Selenium;
using PageObjects.Cart;
using PageObjects.Inventory;
using PageObjects.Login;
using SeleniumAutomation.Core;

namespace TestCases.UC01_Global_Navigation
{
    internal class TC01_07_Reset_App_State : WebDriverBase
    {
        public TC01_07_Reset_App_State(IWebDriver driver) : base(driver) { }

        internal TC01_07_Reset_App_State Check()
        {
            const string product = "Sauce Labs Backpack";

            var login = new Login(Driver);
            var inventory = new Inventory(Driver);
            var cart = new Cart(Driver);

            login.Open();
            login.LoginAs(
                TestConfig.StandardUser.Username,
                TestConfig.StandardUser.Password
            );

            Assert.That(inventory.IsOnInventoryPage(), Is.True,
                "Inventory page did not load.");

            inventory.AddProductToCart(product);

            Assert.That(inventory.GetCartBadgeCount(), Is.EqualTo(1),
                "Cart badge count was not updated after add.");

            WaitUntilClick(() => inventory.Locators.BurgerMenuButton());

            Wait.Until(Driver =>
                Driver.FindElements(By.Id("reset_sidebar_link")).Any()
            );

            WaitUntilClick(() => inventory.Locators.ResetAppStateLink());

            Assert.That(inventory.GetCartBadgeCount(), Is.EqualTo(0),
                "Cart was not cleared after reset app state.");

            inventory.OpenCart();

            Assert.That(cart.GetCartItemCount(), Is.EqualTo(0),
                "Cart was not empty after reset app state.");

            return this;
        }
    }
}