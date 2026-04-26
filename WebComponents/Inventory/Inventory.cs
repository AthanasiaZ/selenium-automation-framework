using OpenQA.Selenium;
using SeleniumAutomation.Core;
using System.Globalization;

namespace PageObjects.Inventory
{
    public class Inventory : WebDriverBase
    {
        public InventoryLocators Locators => new InventoryLocators(Driver);

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

            public IWebElement SortDropdown(string condition = "find")
            {
                return GetElementByCss(".product_sort_container", condition);
            }

            public IWebElement CartIcon(string condition = "clickable")
            {
                return GetElementByCss(".shopping_cart_link", condition);
            }

            public IWebElement CartBadge(string condition = "visible")
            {
                return GetElementByCss(".shopping_cart_badge", condition);
            }

            public IWebElement AddToCartButton(string productName, string condition = "clickable")
            {
                return GetElement(By.Id(BuildButtonId("add-to-cart", productName)), condition);
            }

            public IWebElement RemoveButton(string productName, string condition = "clickable")
            {
                return GetElement(By.Id(BuildButtonId("remove", productName)), condition);
            }

            public IWebElement BurgerMenuButton(string condition = "clickable")
            {
                return GetElementByCss("#react-burger-menu-btn", condition);
            }

            public IWebElement LogoutLink(string condition = "clickable")
            {
                return GetElementByCss("#logout_sidebar_link", condition);
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

            private static string BuildButtonId(string prefix, string productName)
            {
                return $"{prefix}-{NormalizeProductName(productName)}";
            }

            private static string NormalizeProductName(string productName)
            {
                return productName.Trim().ToLowerInvariant().Replace(" ", "-");
            }
            public IWebElement ResetAppStateLink(string condition = "clickable")
            {
                return GetElementByCss("#reset_sidebar_link", condition);
            }

            public IWebElement AboutLink(string condition = "clickable")
            {
                return GetElementByCss("#about_sidebar_link", condition);
            }

            public IReadOnlyCollection<IWebElement> ProductImages()
            {
                return Driver.FindElements(By.CssSelector(".inventory_item_img img"));
            }
            public IReadOnlyCollection<IWebElement> ProductDescriptions()
            {
                return Driver.FindElements(By.CssSelector(".inventory_item_description"));
            }
            public IWebElement InventoryItemByName(string productName, string condition = "find")
            {
                return GetElementByXpath(
                    $"//div[contains(@class,'inventory_item')][.//div[contains(@class,'inventory_item_name') and normalize-space(.)='{productName}']]",
                    condition
                );
            }
        }

        public bool IsOnInventoryPage()
        {
            return Driver.Url.Contains("/inventory.html")
                && Driver.FindElements(By.Id("inventory_container")).Any();
        }

        public bool HasProducts()
        {
            return Locators.InventoryItems().Any();
        }

        public int GetProductCount()
        {
            return Locators.InventoryItems().Count;
        }

        public List<string> GetProductNames()
        {
            return Locators.ProductNames()
                .Select(product => product.Text.Trim())
                .ToList();
        }

        public List<decimal> GetProductPrices()
        {
            return Locators.ProductPrices()
                .Select(price => ParseMoney(price.Text))
                .ToList();
        }

        public void SortByText(string text)
        {
            var dropdown = Locators.SortDropdown("visible");
            WaitUntilClick(() => dropdown);

            var option = dropdown.FindElement(By.XPath($".//option[.='{text}']"));
            WaitUntilClick(() => option);
        }

        public void SortByNameAscending() => SortByText("Name (A to Z)");
        public void SortByNameDescending() => SortByText("Name (Z to A)");
        public void SortByPriceAscending() => SortByText("Price (low to high)");
        public void SortByPriceDescending() => SortByText("Price (high to low)");

        public void AddProductToCart(string productName)
        {
            WaitUntilClick(() => Locators.AddToCartButton(productName));

            Wait.Until(driver =>
                driver.FindElements(By.Id(BuildButtonId("remove", productName))).Any()
            );
        }

        public void RemoveProductFromCart(string productName)
        {
            WaitUntilClick(() => Locators.RemoveButton(productName));

            Wait.Until(driver =>
                driver.FindElements(By.Id(BuildButtonId("add-to-cart", productName))).Any()
            );
        }

        public void OpenCart()
        {
            WaitUntilClick(() => Locators.CartIcon());

            Wait.Until(driver =>
                driver.Url.Contains("/cart.html") &&
                driver.FindElements(By.CssSelector(".cart_list")).Any()
            );
        }

        public int GetCartBadgeCount()
        {
            var badges = Driver.FindElements(By.CssSelector(".shopping_cart_badge"));

            if (!badges.Any())
                return 0;

            return int.Parse(badges.First().Text.Trim());
        }

        public bool IsCartBadgeVisible()
        {
            return Driver.FindElements(By.CssSelector(".shopping_cart_badge")).Any();
        }

        public bool IsProductMarkedAsAdded(string productName)
        {
            return Driver.FindElements(By.Id(BuildButtonId("remove", productName))).Any();
        }

        public string? GetProductButtonText(string productName)
        {
            var addId = BuildButtonId("add-to-cart", productName);
            var removeId = BuildButtonId("remove", productName);

            var buttons = Driver.FindElements(By.CssSelector(
                $"button[id='{addId}'], button[id='{removeId}']"));

            return buttons.FirstOrDefault()?.Text;
        }

        public void Logout()
        {
            WaitUntilClick(() => Locators.BurgerMenuButton());

            Wait.Until(driver =>
                driver.FindElements(By.Id("logout_sidebar_link")).Any()
            );

            WaitUntilClick(() => Locators.LogoutLink());

            Wait.Until(driver =>
                driver.FindElements(By.Id("login-button")).Any()
            );
        }

        private decimal ParseMoney(string text)
        {
            var amount = text.Replace("$", "").Trim();
            return decimal.Parse(amount, CultureInfo.InvariantCulture);
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