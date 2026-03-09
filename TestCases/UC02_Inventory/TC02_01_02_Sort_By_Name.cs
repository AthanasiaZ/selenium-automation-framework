using OpenQA.Selenium;
using SeleniumAutomation.Core;
using WebComponents.Inventory;
using WebComponents.Login;

namespace TestCases.UC02_Inventory
{
    internal class TC02_01_02_Sort_By_Name : WebDriverBase
    {
        internal enum SortDirection
        {
            Ascending,
            Descending
        }

        public TC02_01_02_Sort_By_Name(IWebDriver driver) : base(driver) { }

        internal TC02_01_02_Sort_By_Name Check(SortDirection direction)
        {
            var login = new Login(Driver);
            var inventory = new Inventory(Driver);

            login.Open();
            login.LoginAs(TestConfig.StandardUser.Username, TestConfig.StandardUser.Password);

            Assert.That(inventory.IsOnInventoryPage(), Is.True, "Inventory page did not load.");

            var namesBeforeSorting = inventory.GetProductNames();

            var expectedNames = direction == SortDirection.Ascending
                ? namesBeforeSorting.OrderBy(x => x).ToList()
                : namesBeforeSorting.OrderByDescending(x => x).ToList();

            if (direction == SortDirection.Ascending)
            {
                inventory.SortByNameAscending();
            }
            else
            {
                inventory.SortByNameDescending();
            }

            var actualNames = inventory.GetProductNames();

            Assert.That(
                actualNames,
                Is.EqualTo(expectedNames),
                $"Products were not sorted by name {direction}.");

            return this;
        }
    }
}