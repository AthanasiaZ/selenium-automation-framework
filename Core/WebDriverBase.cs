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

        public IWebElement GetElement(By by, string condition = "find")
        {
            switch (condition.ToLower())
            {
                case "clickable":
                    return Wait.Until(ExpectedConditions.ElementToBeClickable(by));
                case "visible":
                    return Wait.Until(ExpectedConditions.ElementIsVisible(by));
                case "find":
                    return Wait.Until(driver =>
                    {
                        var elements = driver.FindElements(by);
                        return elements.Count > 0 ? elements[0] : null;
                    });
                default:
                    throw new ArgumentException("Invalid condition specified. Use 'clickable', 'visible' or 'find'.");
            }
        }

        public bool IsElementPresent(By by)
        {
            try
            {
                Wait.Until(ExpectedConditions.ElementExists(by));
                return true;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
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

        public void ScrollToElement(
        Func<IWebElement> elementGetter,
        string? scrollableContainerCss = null,
        int offset = 0)
        {
            Wait.Until(driver =>
            {
                try
                {
                    var element = elementGetter();
                    if (element == null)
                        return false;

                    var js = (IJavaScriptExecutor)driver;

                    // 1. If container is specified, scroll within it
                    if (!string.IsNullOrEmpty(scrollableContainerCss))
                    {
                        js.ExecuteScript(@"
                    var el = arguments[0];
                    var container = document.querySelector(arguments[1]);
                    if (container) {
                        var rect = el.getBoundingClientRect();
                        var containerRect = container.getBoundingClientRect();
                        container.scrollTop += (rect.top - containerRect.top) + arguments[2];
                    }
                ", element, scrollableContainerCss, offset);
                    }
                    else
                    {
                        // 2. Automated scrollable parent detection
                        js.ExecuteScript(@"
                    function getScrollableParent(el) {
                        while (el) {
                            var style = getComputedStyle(el);
                            if (/(auto|scroll)/.test(style.overflow + style.overflowY + style.overflowX)) {
                                return el;
                            }
                            el = el.parentElement;
                        }
                        return document.scrollingElement || document.documentElement;
                    }

                    var el = arguments[0];
                    var parent = getScrollableParent(el);

                    if (parent === document.body || parent === document.documentElement) {
                        el.scrollIntoView({block: 'center'});
                    } else {
                        var rect = el.getBoundingClientRect();
                        var parentRect = parent.getBoundingClientRect();
                        parent.scrollTop += (rect.top - parentRect.top) + arguments[1];
                    }
                ", element, offset);
                    }

                    return element.Displayed;
                }
                catch
                {
                    return false;
                }
            });
        }

        public bool IsElementInside(IWebElement inner, IWebElement outer)
        {
            var innerX = inner.Location.X;
            var innerY = inner.Location.Y;
            var innerRight = innerX + inner.Size.Width;
            var innerBottom = innerY + inner.Size.Height;

            var outerX = outer.Location.X;
            var outerY = outer.Location.Y;
            var outerRight = outerX + outer.Size.Width;
            var outerBottom = outerY + outer.Size.Height;

            return innerX >= outerX &&
                   innerY >= outerY &&
                   innerRight <= outerRight &&
                   innerBottom <= outerBottom;
        }
    }
}