using Autofac;
using ProCultura.CrossCutting.L10N;
using ProCultura.WebApiOwin.Filters;
using System.Web.Http.Filters;
using System.Web.Mvc;

namespace ProCultura.WebApiOwin
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new Filters.RequireHttpsAttribute());
        }

        public static void RegisterHttpFilters(HttpFilterCollection filters, IContainer container)
        {
            var localizationService = container.Resolve<ILocalizationService>();
            filters.Add(new ProCulturaExceptionFilterAttribute(localizationService));
            filters.Add(new LocalizationFilterAttribute(localizationService));
        }
    }
}
