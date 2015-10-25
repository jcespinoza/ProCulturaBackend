using System.Web.Http.Filters;
using System.Web.Mvc;
using Autofac;
using ProCultura.CrossCutting.L10N;
using ProCultura.WebApiOwin.Filters;
using RequireHttpsAttribute = ProCultura.WebApiOwin.Filters.RequireHttpsAttribute;

namespace ProCultura.WebApiOwin
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterHttpFilters(HttpFilterCollection filters, IContainer container)
        {
            filters.Add(new RequireHttpsAttribute());

            var localizationService = container.Resolve<ILocalizationService>();
            filters.Add(new ProCulturaExceptionFilterAttribute(localizationService));
            filters.Add(new LocalizationFilterAttribute(localizationService));
        }
    }
}
