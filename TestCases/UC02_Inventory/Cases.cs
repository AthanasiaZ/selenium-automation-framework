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

    }
}