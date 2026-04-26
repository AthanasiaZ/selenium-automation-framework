using SeleniumAutomation.Core;
using TestCases.UC03_Cart;

namespace TestCases.UC04_Checkout
{
    [TestFixture]
    public class Cases : BaseTest
    {
        /// <summary>
        /// Executes the test case that validates the checkout process.
        /// </summary>
        [Test]
        public void TC04_01()
        {
            new TC04_01_Checkout_Validation(Driver).Check();
        }
        /// <summary>
        /// Executes the test case for a successful checkout scenario.
        /// </summary>
        [Test]
        public void TC04_02()
        {
            new TC04_02_Successful_Checkout(Driver).Check();
        }
        /// <summary>
        /// Executes the test case that verifies the accuracy of checkout totals in the application.
        /// </summary>
        [Test]
        public void TC04_03()
        {
            new TC04_03_Checkout_Totals(Driver).Check();
        }
        /// <summary>
        /// Executes the test case that verifies the cancellation process from the checkout overview page.
        /// </summary>
        [Test]
        public void TC04_04()
        {
            new TC04_04_Cancel_From_Checkout_Overview(Driver).Check();
        }

    }
}
