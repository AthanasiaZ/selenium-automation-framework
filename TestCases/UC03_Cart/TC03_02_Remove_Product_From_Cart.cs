using OpenQA.Selenium;
using SeleniumAutomation.Core;
using PageObjects.Login;
using PageObjects.Inventory;
using PageObjects.Cart;

namespace TestCases.UC03_Cart
{
    internal class TC03_02_Remove_Product_From_Cart : WebDriverBase
    {
        public TC03_02_Remove_Product_From_Cart(IWebDriver driver) : base(driver) { }

        internal TC03_02_Remove_Product_From_Cart Check()
        {
            var productsToAdd = new[]
            {
                "Sauce Labs Backpack",
                "Sauce Labs Bike Light"
            };

            const string productToRemove = "Sauce Labs Bike Light";
            const string expectedRemainingProduct = "Sauce Labs Backpack";

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

            Assert.That(cart.GetCartItemCount(), Is.EqualTo(productsToAdd.Length),
                "Cart item count was incorrect before removal.");

            cart.RemoveProduct(productToRemove);

            var cartProductNames = cart.GetCartProductNames();

            Assert.That(cart.GetCartItemCount(), Is.EqualTo(1),
                "Cart item count was incorrect after removal.");

            Assert.That(cartProductNames, Does.Not.Contain(productToRemove),
                $"Removed product '{productToRemove}' was still displayed in cart.");

            Assert.That(cartProductNames, Does.Contain(expectedRemainingProduct),
                $"Remaining product '{expectedRemainingProduct}' was not displayed in cart.");

            Assert.That(inventory.GetCartBadgeCount(), Is.EqualTo(1),
                "Cart badge count was not updated after removing one product from cart.");

            Assert.That(cart.GetCartQuantities(), Is.All.EqualTo(1),
                "One or more remaining cart items had incorrect quantity.");

            return this;
        }
    }
}