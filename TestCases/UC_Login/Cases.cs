using NUnit.Framework;
using SeleniumAutomation.Core;

namespace SeleniumAutomation.TestCases.UC_Login
{
    [TestFixture]
    public class Cases : BaseTest
    {
        [Test]
        public void Login_With_Valid_User()
        {
            new TC_Login_ValidUser(Driver).Check();
        }

        [Test]
        public void Login_With_Invalid_User()
        {
            new TC_Login_InvalidUser(Driver).Check();
        }

        [Test]
        public void Login_With_Empty_Credentials()
        {
            new TC_Login_EmptyCredentials(Driver).Check();
        }
    }
}