using OpenQA.Selenium;
using SeleniumAutomation.Core;
using System.Globalization;

namespace PageObjects.Checkout
{
    public class Checkout : WebDriverBase
    {
        public CheckoutLocators Locators => new CheckoutLocators(Driver);

        public Checkout(IWebDriver driver) : base(driver) { }

        public class CheckoutLocators : WebDriverBase
        {
            public CheckoutLocators(IWebDriver driver) : base(driver) { }

            public IWebElement CheckoutButton(string condition = "clickable")
            {
                return GetElementByCss("#checkout", condition);
            }

            public IWebElement ContinueButton(string condition = "clickable")
            {
                return GetElementByCss("#continue", condition);
            }

            public IWebElement FinishButton(string condition = "clickable")
            {
                return GetElementByCss("#finish", condition);
            }

            public IWebElement FirstName(string condition = "find")
            {
                return GetElementByCss("#first-name", condition);
            }

            public IWebElement LastName(string condition = "find")
            {
                return GetElementByCss("#last-name", condition);
            }

            public IWebElement PostalCode(string condition = "find")
            {
                return GetElementByCss("#postal-code", condition);
            }

            public IWebElement ErrorMessage(string condition = "visible")
            {
                return GetElementByCss("[data-test='error']", condition);
            }

            public IWebElement CompleteHeader(string condition = "visible")
            {
                return GetElementByCss(".complete-header", condition);
            }

            public IWebElement Subtotal(string condition = "visible")
            {
                return GetElementByCss(".summary_subtotal_label", condition);
            }

            public IWebElement Tax(string condition = "visible")
            {
                return GetElementByCss(".summary_tax_label", condition);
            }

            public IWebElement Total(string condition = "visible")
            {
                return GetElementByCss(".summary_total_label", condition);
            }

            public IWebElement BackHomeButton(string condition = "clickable")
            {
                return GetElementByCss("#back-to-products", condition);
            }

            public IWebElement CancelButton(string condition = "clickable")
            {
                return GetElementByCss("#cancel", condition);
            }
        }

        public void StartCheckout()
        {
            ScrollToElement(
                () => Locators.CheckoutButton("find"),
                ".cart_contents_container"
            );

            WaitUntilClick(() => Locators.CheckoutButton());

            Wait.Until(driver =>
                driver.Url.Contains("/checkout-step-one.html") &&
                driver.FindElements(By.Id("first-name")).Any()
            );
        }

        public void FillCheckoutInfo(string firstName, string lastName, string postalCode)
        {
            Locators.FirstName("visible").Clear();
            Locators.FirstName("visible").SendKeys(firstName);

            Locators.LastName("visible").Clear();
            Locators.LastName("visible").SendKeys(lastName);

            Locators.PostalCode("visible").Clear();
            Locators.PostalCode("visible").SendKeys(postalCode);
        }

        public void ContinueToOverview()
        {
            WaitUntilClick(() => Locators.ContinueButton());

            Wait.Until(driver =>
                driver.Url.Contains("/checkout-step-two.html") &&
                driver.FindElements(By.CssSelector(".summary_info")).Any()
            );
        }

        public void ContinueExpectingValidation()
        {
            WaitUntilClick(() => Locators.ContinueButton());

            Wait.Until(driver =>
                driver.FindElements(By.CssSelector("[data-test='error']")).Any()
            );
        }

        public void FinishCheckout()
        {
            WaitUntilClick(() => Locators.FinishButton());

            Wait.Until(driver =>
                driver.Url.Contains("/checkout-complete.html") &&
                driver.FindElements(By.CssSelector(".complete-header")).Any()
            );
        }

        public string GetErrorMessage()
        {
            return Locators.ErrorMessage().Text.Trim();
        }

        public bool IsOrderComplete()
        {
            return Driver.FindElements(By.CssSelector(".complete-header")).Any();
        }

        public decimal GetSubtotal()
        {
            return ParseMoney(Locators.Subtotal().Text);
        }

        public decimal GetTax()
        {
            return ParseMoney(Locators.Tax().Text);
        }

        public decimal GetTotal()
        {
            return ParseMoney(Locators.Total().Text);
        }

        public void BackHome()
        {
            WaitUntilClick(() => Locators.BackHomeButton());

            Wait.Until(driver =>
                driver.Url.Contains("/inventory.html") &&
                driver.FindElements(By.Id("inventory_container")).Any()
            );
        }

        public void CancelFromOverview()
        {
            WaitUntilClick(() => Locators.CancelButton());

            Wait.Until(driver =>
                driver.Url.Contains("/inventory.html") &&
                driver.FindElements(By.Id("inventory_container")).Any()
            );
        }

        private decimal ParseMoney(string text)
        {
            var amount = text.Split('$')[1].Trim();
            return decimal.Parse(amount, CultureInfo.InvariantCulture);
        }
    }
}