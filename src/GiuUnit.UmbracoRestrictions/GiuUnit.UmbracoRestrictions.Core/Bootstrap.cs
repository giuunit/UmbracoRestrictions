using System.IO;
using Umbraco.Core;
using Umbraco.Core.Services;

namespace GiuUnit.UmbracoRestrictions.Core
{
    public class Bootstrap : ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            var appPath = umbracoApplication.Server.MapPath("~/");
            var configFilePath = Path.Combine(appPath, @"config\restrictions.config");

            var config = new RestrictionsConfig(configFilePath);

            ContentService.Published += new RootSingletonDocumentRestriction(config.ConfigEntries).OnPublish;
        }
    }
}
