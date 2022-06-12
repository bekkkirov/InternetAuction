using InternetAuction.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InternetAuction.DAL.Configurations
{
    /// <summary>
    /// Configuration for the image set in database.
    /// </summary>
    public class ImageConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.PublicId)
                   .IsRequired();

            builder.Property(i => i.Url)
                   .IsRequired();

            builder.HasOne(i => i.Lot)
                   .WithMany(l => l.Images)
                   .HasForeignKey(i => i.LotId);
        }
    }
}