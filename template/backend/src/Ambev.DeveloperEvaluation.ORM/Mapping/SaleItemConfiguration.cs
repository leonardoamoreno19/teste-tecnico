using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
    {
        public void Configure(EntityTypeBuilder<SaleItem> builder)
        {
            builder.ToTable("SaleItems");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.ProductId)
                .IsRequired();

            builder.Property(i => i.ProductName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(i => i.Quantity)
                .IsRequired();

            builder.Property(i => i.UnitPrice)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(i => i.DiscountPercentage)
                .IsRequired()
                .HasPrecision(18, 4)
                .HasDefaultValue(0);

            builder.Property(i => i.TotalAmount)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(i => i.IsCancelled)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(i => i.CreatedAt)
                .IsRequired();

            builder.Property(i => i.UpdatedAt);

            builder.Property(i => i.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);

            // Relacionamento com Sale já está configurado na classe SaleConfiguration
        }
    }
} 