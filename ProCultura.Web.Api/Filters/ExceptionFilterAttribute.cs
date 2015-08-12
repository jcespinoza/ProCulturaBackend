namespace ProCultura.Web.Api.Filters
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http.Filters;

    using Procultura.Application.Exceptions;

    using ProCultura.CrossCutting.L10N;
    using ProCultura.CrossCutting.Settings;

    public class ProCulturaExceptionFilterAttribute : ExceptionFilterAttribute, IExceptionFilter
    {
        private static IDictionary<Type, HttpStatusCode> _exceptionDictionary;

        private readonly ILocalizationService _localizationService;

        private static IDictionary<Type, HttpStatusCode> ExceptionDictionary
        {
            get
            {
                return _exceptionDictionary ?? CreateAndInitializeExceptionDictionary();
            }
        }

        public ProCulturaExceptionFilterAttribute(ILocalizationService localizationService)
        {
            this._localizationService = localizationService;
        }

        private static IDictionary<Type, HttpStatusCode> CreateAndInitializeExceptionDictionary()
        {
            var dictionary = new Dictionary<Type, HttpStatusCode>();

            dictionary.Add(typeof(UserNotFoundException), HttpStatusCode.NotFound);
            dictionary.Add(typeof(EmailInUseException), HttpStatusCode.Forbidden);
            dictionary.Add(typeof(NotEnoughPrivilegesException), HttpStatusCode.Forbidden);
            dictionary.Add(typeof(EmptyEmailException), HttpStatusCode.Forbidden);
            dictionary.Add(typeof(InvalidPasswordException), HttpStatusCode.Forbidden);
            dictionary.Add(typeof(ResourceNotFoundException), HttpStatusCode.NotFound);
            return dictionary;

        }

        private HttpStatusCode GetStatusCode(Exception exception)
        {
            if (ExceptionDictionary.ContainsKey(exception.GetType()))
            {
                return ExceptionDictionary[exception.GetType()];
            }
            return HttpStatusCode.InternalServerError;
        }

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnException(actionExecutedContext);
            var exception = actionExecutedContext.Exception;
            var message = _localizationService.GetLocalizedString(exception.Message, AppStrings.EnglishCode);
            actionExecutedContext.Response = new HttpResponseMessage(this.GetStatusCode(exception))
                                                 {
                                                     Content = new StringContent(message)
                                                 };
        }
    }
}