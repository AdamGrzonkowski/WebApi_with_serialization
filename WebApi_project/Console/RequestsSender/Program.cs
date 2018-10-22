using Autofac;
using log4net;
using RequestsSender.Core;
using System;
using System.IO;

namespace RequestsSender
{
    public class Program
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(Program));

        public static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(AppDomain.CurrentDomain.SetupInformation
                .ConfigurationFile));

            try
            {
                Logger.Info("Requests sender started.");

                using (IContainer container = Startup.ConfigureContainer())
                {
                    ILogic logic = container.Resolve<ILogic>();
                    logic.RunAsync(args).GetAwaiter().GetResult();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            finally
            {
                Logger.Info("Requests sender finished.");
            }
        }
    }
}