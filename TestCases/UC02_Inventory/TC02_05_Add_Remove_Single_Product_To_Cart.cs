using OpenQA.Selenium;
using SeleniumAutomation.Core;
using PageObjects.Inventory;
using PageObjects.Login;

namespace TestCases.UC02_Inventory
{
    internal class TC02_05_Add_Remove_Single_Product_To_Cart : WebDriverBase
    {
        public TC02_05_Add_Remove_Single_Product_To_Cart(IWebDriver driver) : base(driver) { }

        internal TC02_05_Add_Remove_Single_Product_To_Cart Check()
        {
            const string productName = "Sauce Labs Backpack";

            var login = new Login(Driver);
            var inventory = new Inventory(Driver);

            login.Open();
            login.LoginAs(TestConfig.StandardUser.Username, TestConfig.StandardUser.Password);

            Assert.That(inventory.IsOnInventoryPage(), Is.True, "Inventory page did not load.");

            inventory.AddProductToCart(productName);

            Assert.That(inventory.IsCartBadgeVisible(), Is.True, "Cart badge was not displayed after adding the product.");
            Assert.That(inventory.GetCartBadgeCount(), Is.EqualTo(1), "Cart badge count was not updated to 1.");
            Assert.That(inventory.IsProductMarkedAsAdded(productName), Is.True, $"Product '{productName}' was not marked as added.");
            Assert.That(inventory.GetProductButtonText(productName), Is.EqualTo("Remove"), $"Product '{productName}' button text was not 'Remove' after add.");

            inventory.RemoveProductFromCart(productName);

            Assert.That(inventory.IsCartBadgeVisible(), Is.False, "Cart badge was still displayed after removing the only product.");
            Assert.That(inventory.GetCartBadgeCount(), Is.EqualTo(0), "Cart badge count was not reset to 0 after removing the only product.");
            Assert.That(inventory.IsProductMarkedAsAdded(productName), Is.False, $"Product '{productName}' was still marked as added after remove.");
            Assert.That(inventory.GetProductButtonText(productName), Is.EqualTo("Add to cart"), $"Product '{productName}' button text was not 'Add to cart' after remove.");

            return this;
        }
    }
}