using Microsoft.Extensions.Configuration;

namespace SeleniumAutomation.Core
{
    public static class TestConfig
    {
        private static readonly IConfigurationRoot config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        public static string BaseUrl =>
            config["TestSettings:BaseUrl"]
            ?? throw new InvalidOperationException("Missing configuration: TestSettings:BaseUrl");

        public static string Browser =>
            config["TestSettings:Browser"]
            ?? throw new InvalidOperationException("Missing configuration: TestSettings:Browser");

        public static string DefaultPassword =>
            config["Authentication:DefaultPassword"]
            ?? throw new InvalidOperationException("Missing configuration: Authentication:DefaultPassword");

        public static string CheckoutFirstName =>
            config["CheckoutData:FirstName"]
            ?? throw new InvalidOperationException("Missing configuration: CheckoutData:FirstName");

        public static string CheckoutLastName =>
            config["CheckoutData:LastName"]
            ?? throw new InvalidOperationException("Missing configuration: CheckoutData:LastName");

        public static string CheckoutPostalCode =>
            config["CheckoutData:PostalCode"]
            ?? throw new InvalidOperationException("Missing configuration: CheckoutData:PostalCode");

        public static class Checkout
        {
            public static string FirstName => config["CheckoutData:FirstName"]!;
            public static string LastName => config["CheckoutData:LastName"]!;
            public static string PostalCode => config["CheckoutData:PostalCode"]!;
        }
        public static UserCredentials GetUser(string userKey)
        {
            if (string.IsNullOrWhiteSpace(userKey))
                throw new ArgumentException("User key cannot be null or empty.", nameof(userKey));

            string usernamePath = $"Users:{userKey}:Username";
            string passwordPath = $"Users:{userKey}:Password";

            string username = config[usernamePath]
                ?? throw new InvalidOperationException($"Missing configuration: {usernamePath}");

            string password = config[passwordPath] ?? DefaultPassword;

            return new UserCredentials(username, password);
        }
        public static UserCredentials StandardUser => GetUser(TestUsers.Standard);
        public static UserCredentials LockedOutUser => GetUser(TestUsers.LockedOut);
        public static UserCredentials ProblemUser => GetUser(TestUsers.Problem);
        public static UserCredentials PerformanceGlitchUser => GetUser(TestUsers.PerformanceGlitch);
        public static UserCredentials ErrorUser => GetUser(TestUsers.Error);
        public static UserCredentials VisualUser => GetUser(TestUsers.Visual);
    }

    public sealed class UserCredentials
    {
        public string Username { get; }
        public string Password { get; }

        public UserCredentials(string username, string password)
        {
            Username = username ?? throw new ArgumentNullException(nameof(username));
            Password = password ?? throw new ArgumentNullException(nameof(password));
        }
    }

    public static class TestUsers
    {
            public const string Standard = "Standard";
            public const string LockedOut = "LockedOut";
            public const string Problem = "Problem";
            public const string PerformanceGlitch = "PerformanceGlitch";
            public const string Error = "Error";
            public const string Visual = "Visual";
    }
}