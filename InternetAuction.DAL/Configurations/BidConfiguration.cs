using InternetAuction.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InternetAuction.DAL.Configurations
{
    public class BidConfiguration : IEntityTypeConfiguration<Bid>
    {
        public void Configure(EntityTypeBuilder<Bid> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.BidValue)
                   .IsRequired();

            builder.Property(b => b.BidTime)
                   .IsRequired();

            builder.HasOne(b => b.Lot)
                   .WithMany(l => l.Bids)
                   .HasForeignKey(b => b.LotId);
                   

            builder.HasOne(b => b.Bidder)
                   .WithMany(bidder => bidder.Bids)
                   .HasForeignKey(b => b.BidderId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}