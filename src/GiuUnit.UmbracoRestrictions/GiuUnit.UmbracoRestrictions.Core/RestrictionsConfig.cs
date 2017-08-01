using log4net;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;

namespace GiuUnit.UmbracoRestrictions.Core
{
    public class RestrictionsConfig
    {
        private readonly static ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public RestrictionsConfigRoot ConfigEntries { get; set; }

        public RestrictionsConfig(string configPath)
        {
            if (!File.Exists(configPath))
            {
                Logger.Warn("Couldn't find config file " + configPath);
                return;
            }

            XmlSerializer serializer = new XmlSerializer(typeof(RestrictionsConfigRoot));
            using (FileStream fileStream = new FileStream(configPath, FileMode.Open))
            {
                RestrictionsConfigRoot result = (RestrictionsConfigRoot)serializer.Deserialize(fileStream);
                ConfigEntries = result;
            }
        }
    }
}
