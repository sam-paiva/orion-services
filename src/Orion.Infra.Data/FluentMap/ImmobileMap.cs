using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orion.Core.Entities.Properties;

namespace Orion.Infra.Data.FluentMap
{
    internal class ImmobileMap : IEntityTypeConfiguration<Immobile>
    {
        public new void Configure(EntityTypeBuilder<Immobile> modelBuilder)
        {
            modelBuilder
           .HasOne<Address>(s => s.Address)
           .WithOne(ad => ad.Immobile)
           .HasForeignKey<Address>(ad => ad.ImmobileId).IsRequired();

            modelBuilder.HasKey(c => c.Id);
            modelBuilder.Property(c => c.Id).ValueGeneratedOnAdd().IsRequired();
            modelBuilder.Property(c => c.Title).HasMaxLength(250).IsRequired();
            modelBuilder.Property(c => c.Description).HasMaxLength(700).IsRequired();
            modelBuilder.Property(c => c.ImmobileType).IsRequired();
            modelBuilder.Property(c => c.CreationDate).IsRequired();
            modelBuilder.Property(c => c.AcquisitionType).IsRequired();
            modelBuilder.Ignore(c => c.ValidationResult);
        }
    }
}
