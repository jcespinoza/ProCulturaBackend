namespace ProCultura.Web.Api
{
    using System.Reflection;
    using System.Web.Http;

    using Autofac;
    using Autofac.Integration.WebApi;

    using ProCultura.CrossCutting.Encryption;
    using ProCultura.CrossCutting.L10N;

    /// <summary>
    /// Register implementations for project dependencies
    /// </summary>
    public class ContainerRegistration
    {
        /// <summary>
        /// Main method for registration of dependencies. It also sets AutoFac to be the dependency resolver.
        /// </summary>
        /// <param name="config">The current HttpConfiguration for this assembly</param>
        public static void Configure(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();

            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // OPTIONAL: Register the Autofac filter provider.
            builder.RegisterWebApiFilterProvider(config);

            RegisterCustomTypes(builder);

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        /// <summary>
        /// Register types written by us
        /// </summary>
        /// <param name="builder"></param>
        private static void RegisterCustomTypes(ContainerBuilder builder)
        {
            ConfigureCrossCuttingDependencies(builder);
            ConfigureDomainDependencies(builder);
            ConfigureDataDependencies(builder);
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
            builder.RegisterType<DatabaseLocalizationService>().As<ILocalizationService>();
            builder.RegisterType<AuthRequestFactory>().As<IAuthRequestFactory>();
            builder.RegisterType<GeneralEncryptionService>().As<IEncryptionService>();
        }

        /// <summary>
        /// Register dependencies registered in the infrastructure layer
        /// </summary>
        /// <param name="builder"></param>
        private static void ConfigureDataDependencies(ContainerBuilder builder)
        {

        }
    }
}