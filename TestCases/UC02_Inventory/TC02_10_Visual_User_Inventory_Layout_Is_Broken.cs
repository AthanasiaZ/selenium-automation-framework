using OpenQA.Selenium;
using SeleniumAutomation.Core;
using PageObjects.Login;
using PageObjects.Inventory;

namespace TestCases.UC02_Inventory
{
    internal class TC02_10_Visual_User_Inventory_Layout_Is_Broken : WebDriverBase
    {
        public TC02_10_Visual_User_Inventory_Layout_Is_Broken(IWebDriver driver) : base(driver) { }

        internal TC02_10_Visual_User_Inventory_Layout_Is_Broken Check()
        {
            const string productName = "Test.allTheThings() T-Shirt (Red)";

            var login = new Login(Driver);
            var inventory = new Inventory(Driver);

            login.Open();
            login.LoginAs(TestConfig.VisualUser.Username, TestConfig.VisualUser.Password);

            Assert.That(inventory.IsOnInventoryPage(), Is.True,
                "Inventory page did not load.");

            var product = inventory.Locators.InventoryItemByName(productName, "find");

            ScrollToElement(() => inventory.Locators.InventoryItemByName(productName));

            product = inventory.Locators.InventoryItemByName(productName, "find");

            var container = product;
            var button = product.FindElement(By.CssSelector("button.btn_inventory"));

            var isInside = IsElementInside(button, container);

            Assert.That(isInside, Is.False,
                "Visual layout issue was not detected: button is still inside container.");

            return this;
        }
    }
}