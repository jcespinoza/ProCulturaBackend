using System.Data.Entity;
using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Procultura.Application.Services.Events;
using ProCultura.CrossCutting.Encryption;
using ProCultura.CrossCutting.L10N;
using ProCultura.CrossCutting.L10N.EF;
using ProCultura.Data.Context;
using ProCultura.Data.Repositories;
using ProCultura.Data.UnitOfWork;
using ProCultura.Domain.Repositories;
using ProCultura.Domain.UnitOfWork;

namespace ProCultura.WebApiOwin
{
    /// <summary>
    /// Register implementations for project dependencies
    /// </summary>
    public class ContainerRegistration
    {
        /// <summary>
        /// Main method for registration of dependencies. It also sets AutoFac to be the dependency resolver.
        /// </summary>
        /// <param name="config">The current HttpConfiguration for this assembly</param>
        public static void Configure(HttpConfiguration config, ContainerBuilder builder)
        {
            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            RegisterCustomTypes(builder);
        }

        /// <summary>
        /// Register types written by us
        /// </summary>
        /// <param name="builder"></param>
        private static void RegisterCustomTypes(ContainerBuilder builder)
        {
            ConfigureDataDependencies(builder);
            ConfigureDomainDependencies(builder);
            ConfigureCrossCuttingDependencies(builder);
            ConfigureApplicationDependencies(builder);
        }

        /// <summary>
        /// Register dependencies defined in the Domain Layer.
        /// </summary>
        /// <param name="builder"></param>
        private static void ConfigureDomainDependencies(ContainerBuilder builder)
        {
            
        }

        /// <summary>
        /// Registers CrossCutting dependencies. This method should be called first.
        /// </summary>
        /// <param name="builder"></param>
        private static void ConfigureCrossCuttingDependencies(ContainerBuilder builder)
        {
            var localizationContext = new LocalizationContext();
            builder.RegisterInstance(localizationContext).AsSelf();

            builder.RegisterType<EntityFrameworkLocalizationService>().As<ILocalizationService>();
            builder.RegisterType<AuthRequestFactory>().As<IAuthRequestFactory>();
            builder.RegisterType<GeneralEncryptionService>().As<IEncryptionService>();
        }

        /// <summary>
        /// Register dependencies registered in the infrastructure layer
        /// </summary>
        /// <param name="builder"></param>
        private static void ConfigureDataDependencies(ContainerBuilder builder)
        {
            var context = new ProCulturaBackEndContext();
            builder.RegisterInstance(context).As<DbContext>();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>));
        }

        /// <summary>
        /// Register dependencies registered in the Application layer
        /// </summary>
        /// <param name="builder"></param>
        private static void ConfigureApplicationDependencies(ContainerBuilder builder)
        {
            builder.RegisterType<EventsAppService>().As<IEventsAppService>();
        }
    }
}