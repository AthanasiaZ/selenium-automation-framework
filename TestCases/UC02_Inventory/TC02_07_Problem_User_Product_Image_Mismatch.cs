using OpenQA.Selenium;
using SeleniumAutomation.Core;
using PageObjects.Login;
using PageObjects.Inventory;

namespace TestCases.UC02_Inventory
{
    internal class TC02_07_Problem_User_Product_Image_Mismatch : WebDriverBase
    {
        public TC02_07_Problem_User_Product_Image_Mismatch(IWebDriver driver) : base(driver) { }

        internal TC02_07_Problem_User_Product_Image_Mismatch Check()
        {
            var login = new Login(Driver);
            var inventory = new Inventory(Driver);

            login.Open();
            login.LoginAs(TestConfig.ProblemUser.Username, TestConfig.ProblemUser.Password);

            Assert.That(inventory.IsOnInventoryPage(), Is.True,
                "Inventory page did not load.");

            var imageSources = inventory.Locators.ProductImages()
                .Select(image => image.GetAttribute("src"))
                .ToList();

            Assert.That(imageSources.Count, Is.EqualTo(inventory.GetProductCount()),
                "Product image count did not match product count.");

            Assert.That(imageSources.Distinct().Count(), Is.EqualTo(1),
                "Problem user did not display identical product images as expected.");

            return this;
        }
    }
}