using InstallmentManager.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InstallmentManager.Infraestructure.Data.Configurations
{
    public class UserConfiguration : BaseConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false)
                .IsRequired();

            builder.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsRequired();

            builder.Property(e => e.Password)
                .HasMaxLength(150)
                .IsUnicode(false)
                .IsRequired();
        }
    }
}
