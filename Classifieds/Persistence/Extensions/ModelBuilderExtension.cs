using Classifieds.Core.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace Classifieds.Persistence.Extensions
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Gry Planszowe" },
                new Category { Id = 2, Name = "Gry RPG" },
                new Category { Id = 3, Name = "Gry Karciane" },
                new Category { Id = 4, Name = "Puzzle" },
                new Category { Id = 5, Name = "Dla dzieci" }
            );
        }
    }
}
