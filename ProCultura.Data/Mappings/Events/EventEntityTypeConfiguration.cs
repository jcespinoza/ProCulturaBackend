namespace ProCultura.Data.Mappings.Events
{
    using System.Data.Entity.ModelConfiguration;

    using Domain.Entities.Events;

    public class EventEntityTypeConfiguration : EntityTypeConfiguration<Event>
    {
        public EventEntityTypeConfiguration()
        {
            //Table
            ToTable("Event", "Events");

            //Primary Key
            HasKey(e => e.EventId);

            //Columns
            Property(e => e.EventId).HasColumnName("EventId").IsRequired();
            Property(e => e.Name).HasColumnName("Name").IsRequired().HasMaxLength(50);
            Property(e => e.ShortDescription).HasColumnName("ShortDescription").HasMaxLength(100).IsRequired();
            Property(e => e.ImageUrl).HasColumnName("ImageUrl");
            Property(e => e.BeginDate).HasColumnName("BeginDate").IsRequired();
            Property(e => e.EndDate).HasColumnName("EndDate").IsRequired();
            Property(e => e.Location).HasColumnName("Location").IsRequired();
            Property(e => e.Status).HasColumnName("Status").IsRequired();
            Property(e => e.LongDescription).HasColumnName("LongDescription").HasMaxLength(1024);
        }
    }
}
