﻿namespace ProCultura.Data.Mappings.Events
{
    using System.Data.Entity.ModelConfiguration;

    using ProCultura.Domain.Entities.Events;

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
            Property(e => e.Summary).HasColumnName("Summary").HasMaxLength(1024);
            Property(e => e.ImageUrl).HasColumnName("ImageUrl");
            Property(e => e.BeginDate).HasColumnName("BeginDate").IsRequired();
            Property(e => e.Location).HasColumnName("Location").IsRequired();
        }
    }
}