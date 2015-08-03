namespace Procultura.Application.Extensions
{
    using System;
    using System.Reflection;

    using Procultura.Application.DTO;

    using ProCultura.CrossCutting.Settings;

    public static class ProCulturaExtensions
    {
        public static void MarkWithException<T>(this ResponseBase dto) where T : Exception, new()
        {
            dto.Exception = new T();
            dto.Message = dto.Exception.Message;
        }

        public static TType MarkedWithException<TType, TException>(this TType dto)
            where TType : ResponseBase
            where TException : Exception, new()
        {
            dto.Exception = new TException();
            dto.Message = dto.Exception.Message;
            return dto;
        }

        public static string GetRequestLanguage<T>(this T request) where T : RequestBase
        {
            if (request.RequestInformation == null
                || string.IsNullOrEmpty(request.RequestInformation.LanguageId) )
            {
                    return AppStrings.EnglishCode;               
            }

            return request.RequestInformation.LanguageId;
        }
    }
}
