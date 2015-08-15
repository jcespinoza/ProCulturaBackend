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

        public string GetLocalizedString(string resourceKey, string languageId)
        {
            //var result = _context.LocalizedEntries.FirstOrDefault()
            return string.Empty;
        }
    }
}
