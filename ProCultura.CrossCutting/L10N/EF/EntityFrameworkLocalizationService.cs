namespace ProCultura.CrossCutting.L10N.EF
{
    using System.Linq;

    public class EntityFrameworkLocalizationService : ILocalizationService
    {

        private readonly LocalizationContext _context;
        public EntityFrameworkLocalizationService(LocalizationContext context)
        {
            this._context = context;
        }

        public string GetLocalizedString(string resourceKey, string languageId = "en")
        {
            var result = _context.LocalizedEntries
                .FirstOrDefault(le => le.EntryKey == resourceKey && languageId.Contains(le.LanguageId));

            if (result != null)
            {
                return result.Value;
            }
            return resourceKey;
        }
    }
}
