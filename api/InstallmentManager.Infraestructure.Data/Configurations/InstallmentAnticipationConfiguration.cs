using InstallmentManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InstallmentManager.Infraestructure.Data.Configurations
{
    public class InstallmentAnticipationConfiguration : BaseConfiguration<InstallmentAnticipation>
    {
        public override void Configure(EntityTypeBuilder<InstallmentAnticipation> builder)
        {
            builder.Property(a => a.Status)
                .IsRequired()
                .HasConversion<string>();

            builder.HasOne(a => a.Installment)
                .WithMany()
                .HasForeignKey(a => a.InstallmentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
