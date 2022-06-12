using InternetAuction.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InternetAuction.DAL.Configurations
{
    /// <summary>
    /// Configuration for the category set in database.
    /// </summary>
    public class LotCategoryConfiguration : IEntityTypeConfiguration<LotCategory>
    {
        public void Configure(EntityTypeBuilder<LotCategory> builder)
        {
            builder.HasKey(lc => lc.Id);

            builder.Property(lc => lc.Name)
                   .HasMaxLength(30)
                   .IsRequired();
        }
    }
}