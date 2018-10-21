using Autofac;
using Autofac.Integration.WebApi;
using Domain.Services;
using Domain.Services.Interfaces;
using Domain.Services.Interfaces.Base;
using Domain.Services.Repositories;
using Domain.Services.Repositories.Base;
using System.Reflection;
using System.Web.Http;

namespace Api.App_Start
{
    /// <summary>
    /// Configuration for IoC container.
    /// </summary>
    public static class ContainerConfig
    {
        public static void Configure()
        {
            var builder = new ContainerBuilder();

            // Get your HttpConfiguration.
            var config = GlobalConfiguration.Configuration;

            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            RegisterServices(builder);

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationContext>().As<IDbContext>();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            builder.RegisterType<RequestRepository>().As<IRequestRepository>();
        }
    }
}