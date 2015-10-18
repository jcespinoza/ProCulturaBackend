using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ProCultura.WebApiOwin.Startup))]

namespace ProCultura.WebApiOwin
{
    using System.Web.Http;

    using Autofac;
    using Autofac.Integration.WebApi;
    using Microsoft.Owin.Cors;
    using System.Threading.Tasks;
    using System.Web.Cors;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var builder = new ContainerBuilder();
            
            // Get your HttpConfiguration. In OWIN, you'll create one
            // rather than using GlobalConfiguration.
            var config = new HttpConfiguration();

            ContainerRegistration.Configure(config, builder);

            // Run other optional steps, like registering filters,
            // per-controller-type services, etc., then set the dependency resolver
            // to be Autofac.
            var container = builder.Build();

            FilterConfig.RegisterHttpFilters(GlobalConfiguration.Configuration.Filters, container);
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            ConfigureCors(app);
            ConfigureAuth(app);

            // Register the Autofac middleware FIRST.
            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(config);
            app.UseWebApi(config);
        }

        private void ConfigureCors(IAppBuilder app)
        {

            var corsOptions = CreateCorsOptions(app);
            app.UseCors(corsOptions);
        }

        private CorsOptions CreateCorsOptions(IAppBuilder app)
        {
            var corsPolicy = new CorsPolicy
            {
                AllowAnyMethod = true,
                AllowAnyHeader = true
            };

            //Add Test Origins and production ones
            corsPolicy.Origins.Add("http://localhost:8090");
            corsPolicy.Origins.Add("http://procultura.herokuapp.com");

            var corsOptions = new CorsOptions
            {
                PolicyProvider = new CorsPolicyProvider
                {
                    PolicyResolver = context => Task.FromResult(corsPolicy)
                }
            };
            return corsOptions;
        }
    }
}
