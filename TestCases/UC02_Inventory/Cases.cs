using SeleniumAutomation.Core;

namespace TestCases.UC02_Inventory
{
    [TestFixture]
    public class Cases : BaseTest
    {
        /// <summary>
        /// Executes the test case that verifies sorting functionality by name in ascending order.
        /// </summary>
        [Test]
        public void TC02_01()
        {
            new TC02_01_02_Sort_By_Name(Driver)
                .Check(TC02_01_02_Sort_By_Name.SortDirection.Ascending);
        }
        /// <summary>
        /// Executes the test case that verifies sorting by name in descending order.
        /// </summary>
        [Test]
        public void TC02_02()
        {
            new TC02_01_02_Sort_By_Name(Driver)
                .Check(TC02_01_02_Sort_By_Name.SortDirection.Descending);
        }
        /// <summary>
        /// Executes the test cases that verifies that sorting by price in ascending order functions as expected.
        /// </summary>
        [Test]
        public void TC02_03()
        {
            new TC02_03_04_Sort_By_Price(Driver)
                .Check(TC02_03_04_Sort_By_Price.SortDirection.Ascending);
        }
        /// <summary>
        /// Executes the test case that verifies sorting by price in descending order.
        /// </summary>
        [Test]
        public void TC02_04()
        {
            new TC02_03_04_Sort_By_Price(Driver)
                .Check(TC02_03_04_Sort_By_Price.SortDirection.Descending);
        }
        /// <summary>
        /// Executes the test case that verifies adding and removing a single product to and from the cart.
        /// </summary>
        [Test]
        public void TC02_05()
        {
            new TC02_05_Add_Remove_Single_Product_To_Cart(Driver).Check();
        }
        [Test]
        public void TC02_06()
        {
            new TC02_06_Add_Remove_Multiple_Products_To_Cart(Driver).Check();
        }
        /// <summary>
        /// Executes the test case that verifies the product image for the problem user scenario is displayed as expected.
        /// </summary>
        [Test]
        public void TC02_07()
        {
            new TC02_07_Problem_User_Product_Image_Mismatch(Driver).Check();
        }
        /// <summary>
        /// Executes the test case that verifies the behavior when removing a product from the cart fails for problem_user.
        /// </summary>
        [Test]
        public void TC02_08()
        {
            new TC02_08_Problem_User_Remove_Product_From_Cart_Fails(Driver).Check();
        }
        /// <summary>
        /// Executes the test case that verifies that the user product images are displayed incorrectly when logged in with visual_user.
        /// </summary>
        [Test]
        public void TC02_09()
        {
            new TC02_09_Visual_User_Product_Images_Are_Incorrect(Driver).Check();
        }
        /// <summary>
        /// Executes the test case that verifies the broken visual layout of the inventory page when logged in with visual_user.
        /// </summary>
        [Test]
        public void TC02_10()
        {
            new TC02_10_Visual_User_Inventory_Layout_Is_Broken(Driver).Check();
        }
    }
}