using OpenQA.Selenium;
using PageObjects.Cart;
using PageObjects.Checkout;
using PageObjects.Inventory;
using PageObjects.Login;
using SeleniumAutomation.Core;

internal class TC04_01_Checkout_Validation : WebDriverBase
{
    public TC04_01_Checkout_Validation(IWebDriver driver) : base(driver) { }

    internal void Check()
    {
        var login = new Login(Driver);
        var inventory = new Inventory(Driver);
        var cart = new Cart(Driver);
        var checkout = new Checkout(Driver);

        login.Open();
        login.LoginAs(TestConfig.StandardUser.Username, TestConfig.StandardUser.Password);

        inventory.AddProductToCart("Sauce Labs Backpack");
        inventory.OpenCart();

        checkout.StartCheckout();
        WaitUntilClick(() => checkout.Locators.ContinueButton());

        Assert.That(checkout.GetErrorMessage(), Does.Contain("First Name"),
            "Expected validation error for missing first name.");
    }
}