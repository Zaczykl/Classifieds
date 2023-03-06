using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Classifieds.Core
{
    public class SortClassifieds
    {
        
        public SortClassifieds()
        {
            SortOptions = new List<SelectListItem>
            {
                new SelectListItem { Text = "Nazwa: A-Z", Value = "NameAsc" },
                new SelectListItem { Text = "Nazwa: Z-A", Value = "NameDesc" },
                new SelectListItem { Text = "Cena: od najniższej", Value = "PriceAsc" },
                new SelectListItem { Text = "Cena: od najwyższej", Value = "PriceDesc" }
            };
        }
        public string SortBy { get; set; }
        public List<SelectListItem> SortOptions { get; set; }

    }
}
