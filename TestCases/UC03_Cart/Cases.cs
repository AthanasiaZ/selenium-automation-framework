using SeleniumAutomation.Core;

namespace TestCases.UC03_Cart
{
    [TestFixture]
    public class Cases : BaseTest
    {
        /// <summary>
        /// Executes the test case that verifies that products added to the cart are correctly displayed.
        /// </summary>
        [Test]
        public void TC03_01()
        {
            new TC03_01_Added_Products_Are_Displayed_In_Cart(Driver).Check();
        }
        /// <summary>
        /// Executes the test case that verifies removing a product from the shopping cart functions as expected.
        /// </summary>
        [Test]
        public void TC03_02()
        {
            new TC03_02_Remove_Product_From_Cart(Driver).Check();
        }

    }
}