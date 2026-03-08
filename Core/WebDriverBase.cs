using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SeleniumAutomation.Core
{
    public class WebDriverBase
    {
        public IWebDriver Driver { get; set; }
        public WebDriverWait Wait { get; set; }
        public Actions Actions { get; set; }

        public WebDriverBase(IWebDriver driver)
        {
            Driver = driver;
            Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            Actions = new Actions(Driver);
        }

        public IWebElement GetElementByCss(string locator, string condition = "find")
        {
            switch (condition.ToLower())
            {
                case "clickable":
                    return Wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(locator)));
                case "visible":
                    return Wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(locator)));
                case "find":
                    return Wait.Until(driver =>
                    {
                        var elements = driver.FindElements(By.CssSelector(locator));
                        return elements.Count > 0 ? elements[0] : null;
                    });
                default:
                    throw new ArgumentException("Invalid condition specified. Use 'clickable', 'visible' or 'find'.");
            }
        }

        public IWebElement GetElementByXpath(string locator, string condition = "find")
        {
            switch (condition.ToLower())
            {
                case "clickable":
                    return Wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(locator)));
                case "visible":
                    return Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(locator)));
                case "find":
                    return Wait.Until(driver => driver.FindElement(By.XPath(locator)));
                default:
                    throw new ArgumentException("Invalid condition specified. Use 'clickable', 'visible' or 'find'.");
            }
        }

        public bool IsElementPresentByCss(string locator)
        {
            try
            {
                Wait.Until(ExpectedConditions.ElementExists(By.CssSelector(locator)));
                return true;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        public bool IsElementPresentByXpath(string locator)
        {
            try
            {
                Wait.Until(ExpectedConditions.ElementExists(By.XPath(locator)));
                return true;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        public void WaitUntilClick(Func<IWebElement> elementGetter)
        {
            Wait.Until(driver =>
            {
                try
                {
                    var element = elementGetter();
                    if (element == null)
                        return null;

                    element.Click();
                    return element;
                }
                catch
                {
                    return null;
                }
            });
        }

        public void WaitUntilSubmit(Func<IWebElement> elementGetter)
        {
            Wait.Until(driver =>
            {
                try
                {
                    var element = elementGetter();
                    if (element == null)
                        return null;

                    element.Submit();
                    return element;
                }
                catch
                {
                    return null;
                }
            });
        }
    }
}