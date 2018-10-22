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
                using (IContainer container = Startup.ConfigureContainer())
                {
                    ILogic logic = container.Resolve<ILogic>();

                    if (args.Length > 1 || args.Length < 1)
                    {
                        logic.DisplayInstructions();
                        Environment.Exit(-1);
                    }

                    Logger.Info("Requests sender started.");
                    logic.RunAsync(args[0]).GetAwaiter().GetResult();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
    }
}