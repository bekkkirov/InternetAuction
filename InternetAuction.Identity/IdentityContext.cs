using InternetAuction.Identity.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InternetAuction.Identity
{
    public class IdentityContext : IdentityDbContext<User, UserRole, int>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
        {

        }


    }
}