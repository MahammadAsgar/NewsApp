

using Microsoft.Extensions.Configuration;

namespace NewsMedia.Application.Config
{
    public static class Configuration
    {
        static public string ConnectionString
        {
            get
            {
                ConfigurationManager configurationManager = new ConfigurationManager();
                configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../NewsMediaApp"));
                configurationManager.AddJsonFile("appsettings.json");

                return configurationManager.GetConnectionString("DefaultyConnection");
            }
        }
    }
}
