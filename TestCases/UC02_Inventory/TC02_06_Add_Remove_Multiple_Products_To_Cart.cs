using OpenQA.Selenium;
using SeleniumAutomation.Core;
using WebComponents.Inventory;
using WebComponents.Login;

namespace TestCases.UC02_Inventory
{
    internal class TC02_06_Add_Remove_Multiple_Products_To_Cart : WebDriverBase
    {
        public TC02_06_Add_Remove_Multiple_Products_To_Cart(IWebDriver driver) : base(driver) { }

        internal TC02_06_Add_Remove_Multiple_Products_To_Cart Check()
        {
            var productsToAdd = new[]
            {
                "Sauce Labs Backpack",
                "Sauce Labs Bike Light",
                "Sauce Labs Bolt T-Shirt"
            };

            const string productToRemove = "Sauce Labs Bike Light";

            var login = new Login(Driver);
            var inventory = new Inventory(Driver);

            login.Open();
            login.LoginAs(TestConfig.StandardUser.Username, TestConfig.StandardUser.Password);

            Assert.That(inventory.IsOnInventoryPage(), Is.True, "Inventory page did not load.");

            foreach (var product in productsToAdd)
            {
                inventory.AddProductToCart(product);
            }

            Assert.That(inventory.IsCartBadgeVisible(), Is.True, "Cart badge was not displayed after adding multiple products.");
            Assert.That(inventory.GetCartBadgeCount(), Is.EqualTo(productsToAdd.Length), "Cart badge count was incorrect after adding multiple products.");

            foreach (var product in productsToAdd)
            {
                Assert.That(inventory.IsProductMarkedAsAdded(product), Is.True, $"Product '{product}' was not marked as added.");
                Assert.That(inventory.GetProductButtonText(product), Is.EqualTo("Remove"), $"Product '{product}' button text was not 'Remove' after add.");
            }

            inventory.RemoveProductFromCart(productToRemove);

            Assert.That(inventory.IsCartBadgeVisible(), Is.True, "Cart badge should still be displayed after removing one of multiple products.");
            Assert.That(inventory.GetCartBadgeCount(), Is.EqualTo(productsToAdd.Length - 1), "Cart badge count was not reduced correctly after removing one product.");
            Assert.That(inventory.IsProductMarkedAsAdded(productToRemove), Is.False, $"Removed product '{productToRemove}' was still marked as added.");
            Assert.That(inventory.GetProductButtonText(productToRemove), Is.EqualTo("Add to cart"), $"Removed product '{productToRemove}' button text was not 'Add to cart'.");

            foreach (var remainingProduct in productsToAdd.Where(p => p != productToRemove))
            {
                Assert.That(inventory.IsProductMarkedAsAdded(remainingProduct), Is.True, $"Remaining product '{remainingProduct}' did not stay in added state.");
                Assert.That(inventory.GetProductButtonText(remainingProduct), Is.EqualTo("Remove"), $"Remaining product '{remainingProduct}' button text was not 'Remove'.");
            }

            return this;
        }
    }
}