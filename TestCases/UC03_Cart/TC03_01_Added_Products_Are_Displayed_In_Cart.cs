using OpenQA.Selenium;
using SeleniumAutomation.Core;
using PageObjects.Login;
using PageObjects.Inventory;
using PageObjects.Cart;

namespace TestCases.UC03_Cart
{
    internal class TC03_01_Added_Products_Are_Displayed_In_Cart : WebDriverBase
    {
        public TC03_01_Added_Products_Are_Displayed_In_Cart(IWebDriver driver) : base(driver) { }

        internal TC03_01_Added_Products_Are_Displayed_In_Cart Check()
        {
            var productsToAdd = new[]
            {
                "Sauce Labs Backpack",
                "Sauce Labs Bike Light"
            };

            var login = new Login(Driver);
            var inventory = new Inventory(Driver);
            var cart = new Cart(Driver);

            login.Open();
            login.LoginAs(TestConfig.StandardUser.Username, TestConfig.StandardUser.Password);

            Assert.That(inventory.IsOnInventoryPage(), Is.True,
                "Inventory page did not load.");

            foreach (var product in productsToAdd)
            {
                inventory.AddProductToCart(product);
            }

            Assert.That(inventory.GetCartBadgeCount(), Is.EqualTo(productsToAdd.Length),
                "Cart badge count was incorrect before opening cart.");

            inventory.OpenCart();

            Assert.That(cart.IsOnCartPage(), Is.True,
                "Cart page did not load.");

            Assert.That(cart.Locators.CartItems().Count, Is.EqualTo(productsToAdd.Length),
                "Cart item count was incorrect.");

            var cartProductNames = cart.GetCartProductNames();

            foreach (var product in productsToAdd)
            {
                Assert.That(cartProductNames, Does.Contain(product),
                    $"Product '{product}' was not displayed in cart.");
            }

            Assert.That(cart.GetCartQuantities(), Is.All.EqualTo(1),
                "One or more cart items had incorrect quantity.");

            Assert.That(cart.GetCartProductPrices(), Has.All.GreaterThan(0),
                "One or more cart items had invalid price.");

            WaitUntilClick(() => cart.Locators.Continue());

            Assert.That(inventory.IsOnInventoryPage(), Is.True,
                "Inventory page did not load after continue shopping.");

            Assert.That(inventory.GetCartBadgeCount(), Is.EqualTo(productsToAdd.Length),
                "Cart badge count was not preserved after continue shopping.");

            foreach (var product in productsToAdd)
            {
                Assert.That(inventory.IsProductMarkedAsAdded(product), Is.True,
                    $"Product '{product}' was not preserved after continue shopping.");
            }

            return this;
        }
    }
}