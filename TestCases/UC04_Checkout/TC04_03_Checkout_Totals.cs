using OpenQA.Selenium;
using OpenQA.Selenium.BiDi.BrowsingContext;
using PageObjects.Checkout;
using PageObjects.Inventory;
using PageObjects.Login;
using SeleniumAutomation.Core;

internal class TC04_03_Checkout_Totals : WebDriverBase
{
    public TC04_03_Checkout_Totals(IWebDriver driver) : base(driver) { }

    internal void Check()
    {
        var login = new Login(Driver);
        var inventory = new Inventory(Driver);
        var checkout = new Checkout(Driver);

        var products = new[]
        {
            "Sauce Labs Backpack",      // 29.99
            "Sauce Labs Bike Light"     // 9.99
        };

        login.Open();
        login.LoginAs(TestConfig.StandardUser.Username, TestConfig.StandardUser.Password);

        foreach (var p in products)
            inventory.AddProductToCart(p);

        inventory.OpenCart();

        checkout.StartCheckout();
        checkout.FillCheckoutInfo(
                TestConfig.Checkout.FirstName,
                TestConfig.Checkout.LastName,
                TestConfig.Checkout.PostalCode);

        WaitUntilClick(() => checkout.Locators.ContinueButton());

        var subtotal = checkout.GetSubtotal();
        var tax = checkout.GetTax();
        var total = checkout.GetTotal();

        Assert.That(subtotal, Is.EqualTo(39.98m),
            "Subtotal calculation is incorrect.");

        Assert.That(total, Is.EqualTo(subtotal + tax),
            "Total is not equal to subtotal + tax.");
    }
}