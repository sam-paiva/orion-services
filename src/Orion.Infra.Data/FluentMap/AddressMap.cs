using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orion.Core.Entities.Properties;

namespace Orion.Infra.Data.FluentMap
{
    internal class AddressMap : IEntityTypeConfiguration<Address>
    {
        public new void Configure(EntityTypeBuilder<Address> modelBuilder)
        {
            modelBuilder.HasKey(c => c.Id);
            modelBuilder.Property(c => c.Id).ValueGeneratedOnAdd().IsRequired();
            modelBuilder.Property(c => c.City).HasMaxLength(150).IsRequired();
            modelBuilder.Property(c => c.State).HasMaxLength(150).IsRequired();
            modelBuilder.Property(c => c.District).HasMaxLength(150).IsRequired();
            modelBuilder.Property(c => c.CreationDate).IsRequired();
            modelBuilder.Ignore(c => c.ValidationResult);
        }
    }
}
