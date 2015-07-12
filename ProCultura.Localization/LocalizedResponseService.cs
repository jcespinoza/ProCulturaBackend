namespace ProCultura.Web.Api.Services
{
    using ProCultura.Web.Api.L10N;

    public static class LocalizedResponseService
    {
        public enum L10N
        {
            English,
            Spanish,
            German,
            French
        }

        static L10N Localization { get; set; }

        public static ILocalizedResponseFactory LocalizedResponseFactory { get; set; }

        public static void SetLocalization(L10N localization)
        {
            Localization = localization;
            switch (Localization)
            {
                case L10N.English:
                    LocalizedResponseFactory = new EnglishResponseFactory();
                    break;
                case L10N.Spanish:
                    LocalizedResponseFactory = new SpanishResponseFactory();
                    break;
                case L10N.French:
                    LocalizedResponseFactory = new FrenchResponseFactory();
                    break;
                case L10N.German:
                    LocalizedResponseFactory = new GermanResponseFactory();
                    break;
            }
        }

        public static L10N GetLocalization()
        {
            return Localization;
        }
    }
}