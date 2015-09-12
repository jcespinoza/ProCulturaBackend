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
        }

        public static void RegisterHttpFilters(HttpFilterCollection filters, IContainer container)
        {
            var localizationService = container.Resolve<ILocalizationService>();
            filters.Add(new ProCulturaExceptionFilterAttribute(localizationService));
            filters.Add(new LocalizationFilterAttribute(localizationService));
        }
    }
}
