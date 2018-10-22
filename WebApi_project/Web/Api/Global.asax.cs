using Api.App_Start;
using System;
using System.IO;
using System.Web.Http;

namespace Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(AppDomain.CurrentDomain.SetupInformation
                .ConfigurationFile));
            GlobalConfiguration.Configure(WebApiConfig.Register);
            ContainerConfig.Configure();
        }
    }
}
