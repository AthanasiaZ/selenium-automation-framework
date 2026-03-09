using SeleniumAutomation.Core;

namespace TestCases.UC02_Inventory
{
    [TestFixture]
    public class Cases : BaseTest
    {
        [Test]
        public void TC02_01()
        {
            new TC02_01_02_Sort_By_Name(Driver)
                .Check(TC02_01_02_Sort_By_Name.SortDirection.Ascending);
        }

        [Test]
        public void TC02_02()
        {
            new TC02_01_02_Sort_By_Name(Driver)
                .Check(TC02_01_02_Sort_By_Name.SortDirection.Descending);
        }

        [Test]
        public void TC02_03()
        {
            new TC02_03_04_Sort_By_Price(Driver)
                .Check(TC02_03_04_Sort_By_Price.SortDirection.Ascending);
        }

        [Test]
        public void TC02_04()
        {
            new TC02_03_04_Sort_By_Price(Driver)
                .Check(TC02_03_04_Sort_By_Price.SortDirection.Descending);
        }

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
    }
}