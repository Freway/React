using Microsoft.Extensions.Configuration;
using System.IO;

namespace BR.POINTER.LASTPOSITION.API.Helpers
{
    public class AppSetting
    {
        static IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(System.AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json");

        /// <summary>
        /// get values from appsettings
        /// </summary>
        public static string Get(string appSetting)
        {                    
            var Parameters = builder.Build();

            var Attributes = Parameters.GetSection("AppSettings");

            return Attributes[appSetting];
        }

        public static string Get(string Section, string tag)
        {
            var Parameters = builder.Build();

            var Attributes = Parameters.GetSection(Section);

            return Attributes[tag];
        }

    }
}