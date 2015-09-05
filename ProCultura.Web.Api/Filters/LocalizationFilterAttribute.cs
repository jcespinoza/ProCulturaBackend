namespace ProCultura.Web.Api.Filters
{
    using System.Web.Http.Filters;

    using ProCultura.CrossCutting.L10N;

    public class LocalizationFilterAttribute : ActionFilterAttribute
    {
        private readonly ILocalizationService _localizationService;

        public LocalizationFilterAttribute(ILocalizationService localizationService)
        {
            _localizationService = localizationService;
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnActionExecuted(actionExecutedContext);
            var response = actionExecutedContext.Response.Content;
            //TODO: Parse the string
        }
    }
}