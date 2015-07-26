namespace ProCultura.CrossCutting.L10N
{
    public interface ILocalizationService
    {
        string GetLocalizedString(string resourceKey, string languageId);
    }
}
