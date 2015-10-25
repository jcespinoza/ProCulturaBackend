using System.Linq;

namespace ProCultura.CrossCutting.L10N.EF
{
    public class EntityFrameworkLocalizationService : ILocalizationService
    {

        private readonly LocalizationContext _context;
        public EntityFrameworkLocalizationService(LocalizationContext context)
        {
            _context = context;
        }

        public string GetLocalizedString(string resourceKey, string languageId = "en")
        {
            if (resourceKey == null) return null;

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
