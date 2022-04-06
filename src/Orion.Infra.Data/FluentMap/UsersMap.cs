using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orion.Core.Entities.Properties;
using Orion.Core.Entities.Users;

namespace Orion.Infra.Data.FluentMap
{
    internal class UsersMap : IEntityTypeConfiguration<User>
    {
        public new void Configure(EntityTypeBuilder<User> modelBuilder)
        {
            modelBuilder
           .HasMany<Immobile>(s => s.Properties)
           .WithOne(g => g.UserOwner)
           .HasForeignKey(s => s.UserId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.HasKey(c => c.Id);
            modelBuilder.Property(c => c.Id).ValueGeneratedOnAdd().IsRequired();
            modelBuilder.Property(c => c.FirstName).HasMaxLength(100).IsRequired();
            modelBuilder.Property(c => c.LastName).HasMaxLength(100).IsRequired();
            modelBuilder.Property(c => c.Email).HasMaxLength(150).IsRequired();
            modelBuilder.Property(c => c.CreationDate).IsRequired();
            modelBuilder.Ignore(c => c.ValidationResult);
        }
    }
}
