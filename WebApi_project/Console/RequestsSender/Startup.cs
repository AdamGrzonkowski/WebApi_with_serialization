using Autofac;
using Helper.Common.Configuration;
using Helper.Common.Http;
using RequestsSender.Core;

namespace RequestsSender
{
    public static class Startup
    {
        public static IContainer ConfigureContainer()
        {
            return CreateContainer();
        }

        private static IContainer CreateContainer()
        {
            ContainerBuilder builder = new ContainerBuilder();

            builder.RegisterType<Configuration>().As<IConfiguration>().SingleInstance();
            builder.RegisterType<Logic>().As<ILogic>().SingleInstance();
            builder.RegisterType<HttpHandler>().As<IHttpHandler>().SingleInstance();

            return builder.Build();
        }
    }
}
