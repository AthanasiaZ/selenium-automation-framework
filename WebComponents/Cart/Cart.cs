using OpenQA.Selenium;
using SeleniumAutomation.Core;
using System.Globalization;

namespace PageObjects.Cart
{
    public class Cart : WebDriverBase
    {
        public CartLocators Locators => new CartLocators(Driver);

        public Cart(IWebDriver driver) : base(driver) { }

        public class CartLocators : WebDriverBase
        {
            public CartLocators(IWebDriver driver) : base(driver) { }

            public IWebElement CartList(string condition = "find")
            {
                return GetElementByCss(".cart_list", condition);
            }
            public IWebElement Continue(string condition = "find")
            {
                return GetElementByCss("#continue-shopping", condition);
            }

            public IWebElement RemoveButton(string productName, string condition = "find")
            {
                return GetElement(By.Id(BuildButtonId("remove", productName)), condition);
            }

            public IReadOnlyCollection<IWebElement> CartItems()
            {
                return Driver.FindElements(By.CssSelector(".cart_item"));
            }

            public IReadOnlyCollection<IWebElement> ProductNames()
            {
                return Driver.FindElements(By.CssSelector(".inventory_item_name"));
            }

            public IReadOnlyCollection<IWebElement> ProductPrices()
            {
                return Driver.FindElements(By.CssSelector(".inventory_item_price"));
            }

            public IReadOnlyCollection<IWebElement> ProductQuantities()
            {
                return Driver.FindElements(By.CssSelector(".cart_quantity"));
            }

            private static string BuildButtonId(string prefix, string productName)
            {
                return $"{prefix}-{NormalizeProductName(productName)}";
            }

            private static string NormalizeProductName(string productName)
            {
                return productName.Trim().ToLowerInvariant().Replace(" ", "-");
            }
        }

        public bool IsOnCartPage()
        {
            return Driver.Url.Contains("/cart.html")
                && Driver.FindElements(By.CssSelector(".cart_list")).Any();
        }

        public int GetCartItemCount()
        {
            return Locators.CartItems().Count;
        }

        public List<string> GetCartProductNames()
        {
            return Locators.ProductNames()
                .Select(e => e.Text.Trim())
                .ToList();
        }

        public List<decimal> GetCartProductPrices()
        {
            return Locators.ProductPrices()
                .Select(e => ParseMoney(e.Text))
                .ToList();
        }

        public List<int> GetCartQuantities()
        {
            return Locators.ProductQuantities()
                .Select(e => int.Parse(e.Text.Trim()))
                .ToList();
        }

        public void RemoveProduct(string productName)
        {
            ScrollToElement(
                () => Locators.RemoveButton(productName),
                ".cart_list"
            );

            WaitUntilClick(() => Locators.RemoveButton(productName));

            Wait.Until(_ => !GetCartProductNames().Contains(productName));
        }

        private decimal ParseMoney(string text)
        {
            var amount = text.Replace("$", "").Trim();
            return decimal.Parse(amount, CultureInfo.InvariantCulture);
        }
    }
}