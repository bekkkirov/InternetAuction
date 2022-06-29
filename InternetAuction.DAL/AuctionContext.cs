using InternetAuction.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace InternetAuction.DAL
{
    /// <summary>
    /// Application main context.
    /// </summary>
    public class AuctionContext : DbContext
    {
        public DbSet<AppUser> Users { get; set; }

        public DbSet<LotCategory> LotCategories { get; set; }

        public DbSet<Lot> Lots { get; set; }

        public DbSet<Bid> Bids { get; set; }

        public DbSet<Image> Images { get; set; }

        public AuctionContext(DbContextOptions<AuctionContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuctionContext).Assembly);
        }
    }
}