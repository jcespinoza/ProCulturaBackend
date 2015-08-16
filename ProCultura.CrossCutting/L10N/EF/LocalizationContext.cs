namespace ProCultura.CrossCutting.L10N.EF
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public class LocalizationContext : DbContext
    {
        public IDbSet<LocalizedEntry> LocalizedEntries { get; set; }

        public LocalizationContext()
            : base("name=ProCulturaBackEndContext")
        {
            
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.Add(new LocalizedEntryEntityTypeConfiguration());
        }
    }
}
