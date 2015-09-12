namespace ProCultura.Web.Api.Filters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using System.Web.Http.Filters;

    using Procultura.Application.DTO;
    using Procultura.Application.Exceptions;
    using Procultura.Application.Extensions;
    using Procultura.Application.Exceptions.Users;

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

        private HttpStatusCode GetStatusCode(Exception exception)
        {
            foreach (var entry in ExceptionDictionary)
            {
                var entryType = entry.Key;
                if (exception.IsDerivedFrom(entryType))
                {
                    return ExceptionDictionary[entry.Key];
                }
            }
            return HttpStatusCode.InternalServerError;
        }

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnException(actionExecutedContext);
            var exception = actionExecutedContext.Exception;
            actionExecutedContext.Response = new HttpResponseMessage(this.GetStatusCode(exception))
                                                 {
                                                     Content = CreateErrorObjectContent(actionExecutedContext)
                                                 };
        }

        private ObjectContent CreateErrorObjectContent(HttpActionExecutedContext actionExecutedContext)
        {
            var errorResponse = CreateErrorResponse(actionExecutedContext);
            return new ObjectContent(typeof(ErrorResponse), errorResponse, new JsonMediaTypeFormatter());
        }

        private ErrorResponse CreateErrorResponse(HttpActionExecutedContext actionExecutedContext)
        {
            var exception = actionExecutedContext.Exception;
            var request = actionExecutedContext.Request;
            var message = _localizationService.GetLocalizedString(exception.Message, GetLanguageFromRequest(request));
            return new ErrorResponse()
                       {
                           Message = message
                       };
        }

        private string GetLanguageFromRequest(HttpRequestMessage request)
        {
            var acceptLanguageHeaderValues = request.Headers.AcceptLanguage;
            var preferredLanguage = acceptLanguageHeaderValues.FirstOrDefault();

            return AppStrings.EnglishCode;
        }

        private static IDictionary<Type, HttpStatusCode> CreateAndInitializeExceptionDictionary()
        {
            _exceptionDictionary =
                new Dictionary<Type, HttpStatusCode>
                {
                    {
                        typeof(ResourceNotFoundException), HttpStatusCode.NotFound
                    },
                    {
                        typeof(DuplicateResourceException), HttpStatusCode.Forbidden
                    },
                    {
                        typeof(NotEnoughPrivilegesException), HttpStatusCode.Forbidden
                    },
                    {
                        typeof(EmptyEmailException), HttpStatusCode.Forbidden
                    },
                    {
                        typeof(InvalidPasswordException), HttpStatusCode.Forbidden
                    }
                };

            return _exceptionDictionary;
        }
    }
}