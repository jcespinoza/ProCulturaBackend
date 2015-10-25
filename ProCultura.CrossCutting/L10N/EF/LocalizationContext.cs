using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ProCultura.CrossCutting.L10N.EF
{
    public class LocalizationContext : DbContext
    {
        public IDbSet<LocalizedEntry> LocalizedEntries { get; set; }

        public LocalizationContext()
            : base("name=ProCulturaContext")
        {
            
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.Add(new LocalizedEntryEntityTypeConfiguration());
        }
    }
}
