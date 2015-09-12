namespace ProCultura.Web.Api.Filters
{
    using System.Linq;
    using System.Net.Http;
    using System.Web.Http.Filters;

    using Procultura.Application.DTO;
    using ProCultura.CrossCutting.L10N;
    using ProCultura.CrossCutting.Settings;

    public class LocalizationFilterAttribute : ActionFilterAttribute
    {
        private readonly ILocalizationService _localizationService;
        public LocalizationFilterAttribute(ILocalizationService localizationService)
        {
            this._localizationService = localizationService;
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnActionExecuted(actionExecutedContext);
            var requestLanguage = GetLanguageFromRequest(actionExecutedContext.Request);
            if (actionExecutedContext.Response != null)
            {
                ReplaceMessageKeys(actionExecutedContext.Response.Content, requestLanguage);
            }
        }

        private string GetLanguageFromRequest(HttpRequestMessage request)
        {
            var headers = request.Headers;
            var acceptLanguageHeader = headers.AcceptLanguage;
            var firstLanguage = acceptLanguageHeader.FirstOrDefault();
            if (firstLanguage != null)
            {
                return firstLanguage.ToString().Substring(0, 2);
            }
            return AppStrings.EnglishCode;
        }

        private void ReplaceMessageKeys(HttpContent httpContent, string languageId)
        {
            var content = httpContent as ObjectContent;
            if (content != null)
            {
                var responseValue = content.Value as ResponseBase;
                if (responseValue != null)
                {
                    var messageKey = responseValue.Message;
                    var replacement = _localizationService.GetLocalizedString(messageKey, languageId);
                    responseValue.Message = replacement;
                }
            }
        }
    }
}