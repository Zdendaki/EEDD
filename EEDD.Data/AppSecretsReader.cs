using Microsoft.Extensions.Configuration;
using ServerData.Database;

namespace ServerData
{
    internal static class AppSecretsReader
    {
        public static T ReadSection<T>(string sectionName)
        {
            var environment = Environment.GetEnvironmentVariable("NETCORE_ENVIRONMENT");
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json").AddJsonFile($"appsettings.{environment}.json", optional: true).AddUserSecrets<Context>().AddEnvironmentVariables();
            var configurationRoot = builder.Build();

            return configurationRoot.GetSection(sectionName).Get<T>();
        }
    }
}
