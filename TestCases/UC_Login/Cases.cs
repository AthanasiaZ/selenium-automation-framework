using NUnit.Framework;
using SeleniumAutomation.Core;

namespace SeleniumAutomation.TestCases.UC01_Login
{
    [TestFixture]
    public class Cases : BaseTest
    {
        [Test]
        public void TC01_01()
        {
            new TC01_01_Login_ValidUser(Driver).Check();
        }

        [Test]
        public void TC01_02()
        {
            new TC01_02_Login_InvalidUser(Driver).Check();
        }

        [Test]
        public void TC01_03()
        {
            new TC01_03_Login_EmptyCredentials(Driver).Check();
        }

        [Test]
        public void TC01_04()
        {
            new TC01_04_Login_EmptyPassword(Driver).Check();
        }

    }
}