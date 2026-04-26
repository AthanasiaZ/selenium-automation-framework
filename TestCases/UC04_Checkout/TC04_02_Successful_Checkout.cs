using OpenQA.Selenium;
using PageObjects.Cart;
using PageObjects.Checkout;
using PageObjects.Inventory;
using PageObjects.Login;
using SeleniumAutomation.Core;

internal class TC04_02_Successful_Checkout : WebDriverBase
{
    public TC04_02_Successful_Checkout(IWebDriver driver) : base(driver) { }

    internal void Check()
    {
        var login = new Login(Driver);
        var inventory = new Inventory(Driver);
        var cart = new Cart(Driver);
        var checkout = new PageObjects.Checkout.Checkout(Driver);

        login.Open();
        login.LoginAs(TestConfig.StandardUser.Username, TestConfig.StandardUser.Password);

        inventory.AddProductToCart("Sauce Labs Backpack");
        inventory.OpenCart();

        checkout.StartCheckout();

        checkout.FillCheckoutInfo(
            TestConfig.Checkout.FirstName,
            TestConfig.Checkout.LastName,
            TestConfig.Checkout.PostalCode
        );
        WaitUntilClick(() => checkout.Locators.ContinueButton());
        WaitUntilClick(() => checkout.Locators.FinishButton());

        Assert.That(checkout.IsOrderComplete(), Is.True,
            "Order was not completed successfully.");

        checkout.BackHome();

        Assert.That(inventory.IsOnInventoryPage(), Is.True,
            "Inventory page did not load after returning home.");
    }
}