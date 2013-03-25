using System.Configuration;

namespace ElkMate.Common
{
    public static class AppSettings
    {
        private static IAppSettingsExtractor Extractor = new AppSettingsExtractor();        
        public static IAppSettingsExtractor AppSettingsExtractor
        {
            get { return Extractor; }
            set { Extractor = value; }
        }
        
        public static string Get(string key)
        {
            return Extractor.Get(key);
        }
    }

    public interface IAppSettingsExtractor
    {
        string Get(string key);
    }

    public class AppSettingsExtractor: IAppSettingsExtractor
    {
        
       public string Get(string key)
       {
 	      return ConfigurationManager.AppSettings[key];
       }
    }
}
