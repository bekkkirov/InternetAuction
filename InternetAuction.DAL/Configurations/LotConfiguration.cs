using System;
using InternetAuction.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InternetAuction.DAL.Configurations
{
    /// <summary>
    /// Configuration for the lot set in database.
    /// </summary>
    public class LotConfiguration : IEntityTypeConfiguration<Lot>
    {
        public void Configure(EntityTypeBuilder<Lot> builder)
        {
            builder.HasKey(l => l.Id);

            builder.Property(l => l.Name)
                   .HasMaxLength(30)
                   .IsRequired();
            
            builder.Property(l => l.Description)
                   .HasMaxLength(250)
                   .IsRequired();

            builder.Property(l => l.InitialPrice)
                   .IsRequired();

            builder.Property(l => l.SaleStartTime)
                   .IsRequired();
            
            builder.Property(l => l.SaleEndTime)
                   .IsRequired();

            builder.Property(l => l.Quantity)
                   .IsRequired();

            builder.HasOne(l => l.Category)
                   .WithMany(c => c.Lots)
                   .HasForeignKey(l => l.CategoryId);

            builder.HasOne(l => l.Seller)
                   .WithMany(s => s.RegisteredLots)
                   .HasForeignKey(l => l.SellerId);

            builder.HasOne(l => l.Buyer)
                   .WithMany(b => b.BoughtLots)
                   .HasForeignKey(l => l.BuyerId);
        }
    }
}