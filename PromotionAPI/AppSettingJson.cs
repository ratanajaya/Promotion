using PromotionAPI.Models;

namespace PromotionAPI
{
    public class AppSettingJson
    {
        public static IConfigurationRoot GetConfigurationSetting() {
            string applicationExeDirectory = Directory.GetCurrentDirectory();

            var builder = new ConfigurationBuilder()
            .SetBasePath(applicationExeDirectory)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            return builder.Build();
        }

        public static string GetConnectionString() {
            return GetConfigurationSetting()["ConnectionStrings:DefaultConnection"];
        }

        public static GlobalVariable GetGlobalVariable() {
            return new GlobalVariable() {
                ConnectionString = GetConfigurationSetting()["ConnectionStrings:DefaultConnection"],
            };
        }
    }
}
