using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.ToTable("Sales");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.SaleNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(s => s.SaleDate)
                .IsRequired();

            builder.Property(s => s.CustomerId)
                .IsRequired();

            builder.Property(s => s.CustomerName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(s => s.BranchId)
                .IsRequired();

            builder.Property(s => s.BranchName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(s => s.TotalAmount)
                .IsRequired()
                .HasPrecision(18, 2);

            builder.Property(s => s.IsCancelled)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(s => s.CreatedAt)
                .IsRequired();

            builder.Property(s => s.UpdatedAt);

            builder.Property(s => s.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);

            // Relacionamento com SaleItem
            builder.HasMany(s => s.Items)
                .WithOne(i => i.Sale)
                .HasForeignKey(i => i.SaleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
} 