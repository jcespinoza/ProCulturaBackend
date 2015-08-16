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

    public class ProCulturaExceptionFilterAttribute : ExceptionFilterAttribute
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
            _exceptionDictionary =
                new Dictionary<Type, HttpStatusCode>
                {
                    {
                        typeof(UserNotFoundException), HttpStatusCode.NotFound
                    },
                    {
                        typeof(EmailInUseException), HttpStatusCode.Forbidden
                    },
                    {
                        typeof(NotEnoughPrivilegesException), HttpStatusCode.Forbidden
                    },
                    {
                        typeof(EmptyEmailException), HttpStatusCode.Forbidden
                    },
                    {
                        typeof(InvalidPasswordException), HttpStatusCode.Forbidden
                    },
                    {
                        typeof(ResourceNotFoundException), HttpStatusCode.NotFound
                    }
                };

            return _exceptionDictionary;
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