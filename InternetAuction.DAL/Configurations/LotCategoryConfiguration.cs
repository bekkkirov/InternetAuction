using InternetAuction.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InternetAuction.DAL.Configurations
{
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