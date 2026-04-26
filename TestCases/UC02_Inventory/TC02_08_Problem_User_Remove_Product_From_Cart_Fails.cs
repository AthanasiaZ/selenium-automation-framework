using OpenQA.Selenium;
using SeleniumAutomation.Core;
using PageObjects.Login;
using PageObjects.Inventory;

namespace TestCases.UC02_Inventory
{
    internal class TC02_08_Problem_User_Remove_Product_From_Cart_Fails : WebDriverBase
    {
        public TC02_08_Problem_User_Remove_Product_From_Cart_Fails(IWebDriver driver) : base(driver) { }

        internal TC02_08_Problem_User_Remove_Product_From_Cart_Fails Check()
        {
            const string productName = "Sauce Labs Backpack";

            var login = new Login(Driver);
            var inventory = new Inventory(Driver);

            login.Open();
            login.LoginAs(TestConfig.ProblemUser.Username, TestConfig.ProblemUser.Password);

            Assert.That(inventory.IsOnInventoryPage(), Is.True,
                "Inventory page did not load.");

            inventory.AddProductToCart(productName);

            Assert.That(inventory.GetCartBadgeCount(), Is.EqualTo(1),
                "Cart badge count was not updated after adding product.");

            WaitUntilClick(() => inventory.Locators.RemoveButton(productName));

            Assert.That(inventory.GetCartBadgeCount(), Is.EqualTo(1),
                "Known bug was not reproduced: cart badge changed after remove for problem user.");

            Assert.That(inventory.IsProductMarkedAsAdded(productName), Is.True,
                "Known bug was not reproduced: product was removed for problem user.");

            return this;
        }
    }
}