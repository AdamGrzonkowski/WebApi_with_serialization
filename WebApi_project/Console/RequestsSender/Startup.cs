using Autofac;
using Helper.Common.Configuration;

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

            return builder.Build();
        }
    }
}
