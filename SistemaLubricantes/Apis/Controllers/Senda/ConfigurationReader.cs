using Microsoft.Extensions.Configuration;
using System.IO;

namespace Apis.Controllers.Senda
{
    public class ConfigurationReader
    {
        public static string GetKeyValueAppsetting(string appjsonfile, string section, string key)
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(appjsonfile, false).Build();
            var key_value = configuration[section + ":" + key];
            return key_value;
        }
    }
}
