using OpenQA.Selenium;
using SeleniumAutomation.Core;

namespace WebComponents.Inventory
{
    public class Inventory : WebDriverBase
    {
        public InventoryLocators Locators
        {
            get { return new InventoryLocators(Driver); }
        }

        public Inventory(IWebDriver driver) : base(driver) { }

        public class InventoryLocators : WebDriverBase
        {
            public InventoryLocators(IWebDriver driver) : base(driver) { }

            public IWebElement InventoryContainer(string condition = "find")
            {
                return GetElementByCss("#inventory_container", condition);
            }

            public IWebElement InventoryItemsContainer(string condition = "find")
            {
                return GetElementByCss(".inventory_list", condition);
            }

            public IReadOnlyCollection<IWebElement> InventoryItems()
            {
                return Driver.FindElements(By.CssSelector(".inventory_item"));
            }

            public IReadOnlyCollection<IWebElement> ProductNames()
            {
                return Driver.FindElements(By.CssSelector(".inventory_item_name"));
            }

            public IReadOnlyCollection<IWebElement> ProductPrices()
            {
                return Driver.FindElements(By.CssSelector(".inventory_item_price"));
            }

            public IWebElement SortDropdown(string condition = "find")
            {
                return GetElementByCss(".product_sort_container", condition);
            }

            public IWebElement CartIcon(string condition = "find")
            {
                return GetElementByCss(".shopping_cart_link", condition);
            }

            public IWebElement CartBadge(string condition = "find")
            {
                return GetElementByCss(".shopping_cart_badge", condition);
            }

            public IWebElement AddToCartButton(string productName, string condition = "find")
            {
                return GetElementByCss($"#{BuildButtonId("add-to-cart", productName)}", condition);
            }

            public IWebElement RemoveButton(string productName, string condition = "find")
            {
                return GetElementByCss($"#{BuildButtonId("remove", productName)}", condition);
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

        public bool IsOnInventoryPage()
        {
            return IsElementPresentByCss("#inventory_container");
        }

        public bool HasProducts()
        {
            return Locators.InventoryItems().Count > 0;
        }

        public int GetProductCount()
        {
            return Locators.InventoryItems().Count;
        }

        public void SortByText(string text)
        {
            var dropdown = Locators.SortDropdown("visible");
            dropdown.Click();

            var option = dropdown.FindElement(By.XPath($".//option[.='{text}']"));
            option.Click();
        }

        public void SortByNameAscending() => SortByText("Name (A to Z)");
        public void SortByNameDescending() => SortByText("Name (Z to A)");
        public void SortByPriceAscending() => SortByText("Price (low to high)");
        public void SortByPriceDescending() => SortByText("Price (high to low)");

        public List<string> GetProductNames()
        {
            return Locators.ProductNames()
                .Select(e => e.Text.Trim())
                .ToList();
        }

        public List<decimal> GetProductPrices()
        {
            return Locators.ProductPrices()
                .Select(e => decimal.Parse(e.Text.Replace("$", "").Trim()))
                .ToList();
        }

        public void AddProductToCart(string productName)
        {
            var button = Locators.AddToCartButton(productName, "clickable");
            button.Click();

            Wait.Until(_ => IsElementPresentByCss($"#{BuildButtonId("remove", productName)}"));
        }

        public void RemoveProductFromCart(string productName)
        {
            var button = Locators.RemoveButton(productName, "clickable");
            button.Click();

            Wait.Until(_ => IsElementPresentByCss($"#{BuildButtonId("add-to-cart", productName)}"));
        }
        public int GetCartBadgeCount()
        {
            if (!IsElementPresentByCss(".shopping_cart_badge"))
                return 0;

            return int.Parse(Locators.CartBadge("visible").Text.Trim());
        }

        public bool IsCartBadgeVisible()
        {
            return IsElementPresentByCss(".shopping_cart_badge");
        }

        public void OpenCart()
        {
            var cart = Locators.CartIcon("clickable");
            cart.Click();
        }

        public bool IsProductMarkedAsAdded(string productName)
        {
            return IsElementPresentByCss($"#{BuildButtonId("remove", productName)}");
        }

        public string GetProductButtonText(string productName)
        {
            if (IsElementPresentByCss($"#{BuildButtonId("remove", productName)}"))
                return Locators.RemoveButton(productName, "visible").Text.Trim();

            if (IsElementPresentByCss($"#{BuildButtonId("add-to-cart", productName)}"))
                return Locators.AddToCartButton(productName, "visible").Text.Trim();

            throw new NoSuchElementException($"No cart action button found for product '{productName}'.");
        }

        private string BuildButtonId(string prefix, string productName)
        {
            return $"{prefix}-{NormalizeProductName(productName)}";
        }

        private string NormalizeProductName(string productName)
        {
            return productName.Trim().ToLowerInvariant().Replace(" ", "-");
        }
    }
}