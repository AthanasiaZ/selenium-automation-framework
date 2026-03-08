using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace SeleniumAutomation.Core
{
    public static class TestConfig
    {
        private static readonly IConfigurationRoot config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        public static string BaseUrl => config["TestSettings:BaseUrl"]!;
        public static string Browser => config["TestSettings:Browser"]!;

        public static string ValidUsername => config["Users:ValidUser:Username"]!;
        public static string ValidPassword => config["Users:ValidUser:Password"]!;

        public static string InvalidUsername => config["Users:InvalidUser:Username"]!;
        public static string InvalidPassword => config["Users:InvalidUser:Password"]!;
    }
}