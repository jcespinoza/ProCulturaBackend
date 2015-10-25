namespace ProCultura.WebApiOwin
{
    using System.Data.Entity;
    using System.Reflection;
    using System.Web.Http;

    using Autofac;
    using Autofac.Integration.WebApi;

    using Procultura.Application.Services.Events;
    using Procultura.Application.Services.Users;

    using CrossCutting.Encryption;
    using CrossCutting.L10N;
    using CrossCutting.L10N.EF;
    using Data.Context;
    using Data.Repositories;
    using Data.UnitOfWork;
    using Domain.Repositories;
    using Domain.UnitOfWork;

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
            builder.RegisterType<UserAppService>().As<IUserAppService>();
            builder.RegisterType<EventsAppService>().As<IEventsAppService>();
        }
    }
}