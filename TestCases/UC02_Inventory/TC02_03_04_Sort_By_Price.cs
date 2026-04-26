using OpenQA.Selenium;
using SeleniumAutomation.Core;
using PageObjects.Inventory;
using PageObjects.Login;

namespace TestCases.UC02_Inventory
{
    internal class TC02_03_04_Sort_By_Price : WebDriverBase
    {
        internal enum SortDirection
        {
            Ascending,
            Descending
        }

        public TC02_03_04_Sort_By_Price(IWebDriver driver) : base(driver) { }

        internal TC02_03_04_Sort_By_Price Check(SortDirection direction)
        {
            var login = new Login(Driver);
            var inventory = new Inventory(Driver);

            login.Open();
            login.LoginAs(TestConfig.StandardUser.Username, TestConfig.StandardUser.Password);

            Assert.That(inventory.IsOnInventoryPage(), Is.True, "Inventory page did not load.");

            var pricesBeforeSorting = inventory.GetProductPrices();

            var expectedPrices = direction == SortDirection.Ascending
                ? pricesBeforeSorting.OrderBy(x => x).ToList()
                : pricesBeforeSorting.OrderByDescending(x => x).ToList();

            if (direction == SortDirection.Ascending)
            {
                inventory.SortByPriceAscending();
            }
            else
            {
                inventory.SortByPriceDescending();
            }

            var actualPrices = inventory.GetProductPrices();

            Assert.That(
                actualPrices,
                Is.EqualTo(expectedPrices),
                $"Products were not sorted by price {direction}.");

            return this;
        }
    }
}