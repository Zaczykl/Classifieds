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

        int SaveChanges();
    }
}
