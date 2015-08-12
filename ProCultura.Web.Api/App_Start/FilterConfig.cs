using System.Web.Mvc;

namespace ProCultura.Web.Api
{
    using System.Web.Http.Filters;

    using Autofac;

    using ProCultura.CrossCutting.L10N;
    using ProCultura.Web.Api.Filters;

    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //var builder = new ContainerBuilder();
            //var container = builder.Build();

            //using(var scope = container.BeginLifetimeScope())
            //{
                //var service = scope.Resolve<ILocalizationService>();
                //filters.Add(new ProCulturaExceptionFilterAttribute(new DatabaseLocalizationService()));
            //}
        }

        public static void RegisterHttpFilters(HttpFilterCollection filters)
        {
            filters.Add(new ProCulturaExceptionFilterAttribute(new DatabaseLocalizationService()));
        }
    }
}
