using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using InstallmentManager.Domain.Entities;

namespace InstallmentManager.Infraestructure.Data.Configurations
{
    public class InstallmentConfiguration : BaseConfiguration<Installment>
    {
        public override void Configure(EntityTypeBuilder<Installment> builder)
        {
            builder.Property(i => i.Code)
            .IsRequired()
            .HasMaxLength(50);

            builder.Property(i => i.DueDate)
                .IsRequired();

            builder.Property(i => i.Amount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(i => i.Status)
                .IsRequired();

            builder.Property(i => i.Anticipated)
                .IsRequired();

            builder.Property(i => i.ContractId)
                .IsRequired();

            builder.HasOne(i => i.Contract)
                .WithMany(c => c.Installments)
                .HasForeignKey(i => i.ContractId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
