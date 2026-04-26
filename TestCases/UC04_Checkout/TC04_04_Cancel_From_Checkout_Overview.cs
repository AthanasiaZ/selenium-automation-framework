using OpenQA.Selenium;
using SeleniumAutomation.Core;
using PageObjects.Login;
using PageObjects.Inventory;
using PageObjects.Checkout;

namespace TestCases.UC04_Checkout
{
    internal class TC04_04_Cancel_From_Checkout_Overview : WebDriverBase
    {
        public TC04_04_Cancel_From_Checkout_Overview(IWebDriver driver) : base(driver) { }

        internal TC04_04_Cancel_From_Checkout_Overview Check()
        {
            const string productName = "Sauce Labs Backpack";

            var login = new Login(Driver);
            var inventory = new Inventory(Driver);
            var checkout = new Checkout(Driver);

            login.Open();
            login.LoginAs(TestConfig.StandardUser.Username, TestConfig.StandardUser.Password);

            Assert.That(inventory.IsOnInventoryPage(), Is.True,
                "Inventory page did not load.");

            inventory.AddProductToCart(productName);
            inventory.OpenCart();

            checkout.StartCheckout();

            checkout.FillCheckoutInfo(
                TestConfig.Checkout.FirstName,
                TestConfig.Checkout.LastName,
                TestConfig.Checkout.PostalCode
            );

            WaitUntilClick(() => checkout.Locators.ContinueButton());

            checkout.CancelFromOverview();

            Assert.That(inventory.IsOnInventoryPage(), Is.True,
                "Inventory page did not load after cancelling checkout.");

            Assert.That(inventory.GetCartBadgeCount(), Is.EqualTo(1),
                "Cart badge count was not preserved after cancelling checkout.");

            Assert.That(inventory.IsProductMarkedAsAdded(productName), Is.True,
                $"Product '{productName}' was not preserved in cart after cancelling checkout.");

            return this;
        }
    }
}