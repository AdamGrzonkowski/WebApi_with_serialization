using Api.App_Start;
using log4net;
using System;
using System.IO;
using System.Web;
using System.Web.Http;

namespace Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(WebApiApplication));

        /// <summary>
        /// Entry point for application.
        /// </summary>
        protected void Application_Start()
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(AppDomain.CurrentDomain.SetupInformation
                .ConfigurationFile));
            GlobalConfiguration.Configure(WebApiConfig.Register);
            ContainerConfig.Configure();
        }

        /// <summary>
        /// Global error handler (unhandled exceptions will come through here).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = HttpContext.Current.Server.GetLastError();
            _logger.Error(ex);
        }
    }
}
