using InternetAuction.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InternetAuction.DAL.Configurations
{
    /// <summary>
    /// Configuration for the user set in database.
    /// </summary>
    public class UserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasKey(u => u.Id);

            builder.HasIndex(u => u.UserName)
                   .IsUnique();

            builder.Property(u => u.UserName)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.Property(u => u.FirstName)
                   .HasMaxLength(30)
                   .IsRequired();
            
            builder.Property(u => u.LastName)
                   .HasMaxLength(30)
                   .IsRequired();

            builder.Property(u => u.Balance)
                   .IsRequired();

            builder.HasOne(u => u.ProfileImage)
                   .WithOne(i => i.User)
                   .HasForeignKey<Image>(i => i.UserId);
        }
    }
}