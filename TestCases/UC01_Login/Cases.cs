using SeleniumAutomation.Core;

namespace TestCases.UC01_Global_Navigation
{
    [TestFixture]
    public class Cases : BaseTest
    {
        /// <summary>
        /// Executes the test case that verifies successful login with valid user credentials.
        /// </summary>
        [Test]
        public void TC01_01()
        {
            new TC01_01_Login_ValidUser(Driver).Check();
        }
        /// <summary>
        /// Executes the test case that verifies the login process fails when an invalid password is provided.
        /// </summary>
        [Test]
        public void TC01_02()
        {
            new TC01_02_Login_InvalidPassword(Driver).Check();
        }
        /// <summary>
        /// Executes the test case that verifies the login behavior when empty credentials are provided.
        /// </summary>
        [Test]
        public void TC01_03()
        {
            new TC01_03_Login_EmptyCredentials(Driver).Check();
        }
        /// <summary>
        /// Executes the test case that verifies the login behavior when the password field is left empty.
        /// </summary>
        [Test]
        public void TC01_04()
        {
            new TC01_04_Login_EmptyPassword(Driver).Check();
        }
        /// <summary>
        /// Executes the test case that verifies the login behavior for a locked-out user.
        /// </summary>
        [Test]
        public void TC01_05()
        {
            new TC01_05_Login_LockedOutUser(Driver).Check();
        }
        /// <summary>
        /// Executes the test case that verifies the logout functionality using the current
        /// WebDriver instance.
        /// </summary>
        [Test]
        public void TC01_06()
        {
            new TC01_06_Logout(Driver).Check();
        }
        /// <summary>
        /// Executes the test case that verifies that the application state is reset as expected.
        /// </summary>
        [Test]
        public void TC01_07()
        {
            new TC01_07_Reset_App_State(Driver).Check();
        }
        /// <summary>
        /// Executes the test case that verifies the behavior of the About link in the application.
        /// </summary>
        [Test]
        public void TC01_08()
        {
            new TC01_08_About_Link(Driver).Check();
        }
        /// <summary>
        /// Executes the test case that verifies the login process for the performance glitch user is slower than expected.
        /// </summary>
        [Test]
        public void TC01_09()
        {
            new TC01_09_Performance_Glitch_User_Login_Is_Slower(Driver).Check();
        }

    }
}