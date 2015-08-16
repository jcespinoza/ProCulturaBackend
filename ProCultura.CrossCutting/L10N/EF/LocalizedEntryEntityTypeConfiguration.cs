namespace ProCultura.CrossCutting.L10N.EF
{
    using System.Data.Entity.ModelConfiguration;

    public class LocalizedEntryEntityTypeConfiguration : EntityTypeConfiguration<LocalizedEntry>
    {
        public LocalizedEntryEntityTypeConfiguration()
        {
            HasKey(l => new { l.EntryKey, l.LanguageId });

            Property(l => l.EntryKey).HasColumnName("EntryKey").HasMaxLength(50);
            Property(l => l.LanguageId).HasColumnName("LanguageId").HasMaxLength(4);
            Property(l => l.Value).HasColumnName("Value");

            ToTable("Dictionary", "Localization");
        }
    }
}
