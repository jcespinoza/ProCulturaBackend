namespace Procultura.Application.Extensions
{
    using System;
    using System.Collections.Generic;

    using AutoMapper;

    using Procultura.Application.DTO;

    using ProCultura.CrossCutting.Settings;

    public static class ProCulturaExtensions
    {
        public static string GetRequestLanguage<T>(this T request) where T : RequestBase
        {
            if (request.RequestInformation == null
                || string.IsNullOrEmpty(request.RequestInformation.LanguageId) )
            {
                    return AppStrings.EnglishCode;               
            }

            return request.RequestInformation.LanguageId;
        }

        public static TDestination ProjectAs<TDestination>(this object source) where TDestination : class
        {
            if (source == null)
                throw new ArgumentNullException("source");
            return Mapper.Map<TDestination>(source);
        }

        public static TDestination ReplaceValues<TSource, TDestination>(this TSource source, TDestination destination)
            where TDestination: class
            where TSource  : class
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (source == null)
                throw new ArgumentNullException("destination");
            return Mapper.Map(source, destination);
        }

        public static IEnumerable<TDestination> ProjectAs<TDestination>(this IEnumerable<object> source)
            where TDestination : class
        {
            if (source == null)
                throw new ArgumentNullException("source");
            return Mapper.Map<IEnumerable<TDestination>>(source);
        }
    }
}
