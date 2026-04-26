using OpenQA.Selenium;
using SeleniumAutomation.Core;
using PageObjects.Login;
using PageObjects.Inventory;

namespace TestCases.UC02_Inventory
{
    internal class TC02_09_Visual_User_Product_Images_Are_Incorrect : WebDriverBase
    {
        public TC02_09_Visual_User_Product_Images_Are_Incorrect(IWebDriver driver) : base(driver) { }

        internal TC02_09_Visual_User_Product_Images_Are_Incorrect Check()
        {
            var login = new Login(Driver);
            var inventory = new Inventory(Driver);

            login.Open();
            login.LoginAs(
                TestConfig.VisualUser.Username,
                TestConfig.VisualUser.Password
            );

            Assert.That(inventory.IsOnInventoryPage(), Is.True,
                "Inventory page did not load.");

            var products = Driver.FindElements(By.CssSelector(".inventory_item"));

            Assert.That(products.Count, Is.GreaterThan(0),
                "No products found in inventory.");

            var mismatches = new List<string>();

            foreach (var product in products)
            {
                var name = product
                    .FindElement(By.CssSelector(".inventory_item_name"))
                    .Text
                    .ToLower();

                var imgSrc = product
                    .FindElement(By.CssSelector("img"))
                    .GetAttribute("src")!
                    .ToLower();

                // image src should contain product keyword
                if (name.Contains("backpack") && !imgSrc.Contains("backpack"))
                    mismatches.Add(name);

                if (name.Contains("bike") && !imgSrc.Contains("bike"))
                    mismatches.Add(name);

                if (name.Contains("shirt") && !imgSrc.Contains("shirt"))
                    mismatches.Add(name);

                if (name.Contains("jacket") && !imgSrc.Contains("jacket"))
                    mismatches.Add(name);

                if (name.Contains("onesie") && !imgSrc.Contains("onesie"))
                    mismatches.Add(name);
            }

            Assert.That(mismatches, Is.Not.Empty,
                "Visual user image mismatch bug was not detected.");

            return this;
        }
    }
}