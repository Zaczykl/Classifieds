using Classifieds.Core.Cipher;
using Classifieds.Core.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace Classifieds.Core
{
    public interface IApplicationDbContext
    {
        DbSet<Category> Categories { get; set; }
        DbSet<Classified> Classifieds { get; set; }
        DbSet<ProductImage> ProductImages { get; set; }
        DbSet<ApplicationUser> Users { get; set; }
        DbSet<PasswordData> Passwords { get; set; }
        DbSet<Models.Domains.Email> Emails { get; set; }


        int SaveChanges();
    }
}
