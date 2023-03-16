using Classifieds.Core;
using Classifieds.Core.Cipher;
using Classifieds.Core.Models.Domains;
using Classifieds.Persistence.Extensions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Classifieds.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Classified> Classifieds { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<PasswordData> Passwords { get; set; }
        public DbSet<Email> Emails { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Seed();

            base.OnModelCreating(builder);
        }
    }
}
