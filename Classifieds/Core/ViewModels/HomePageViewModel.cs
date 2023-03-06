using Classifieds.Core.Models;
using Classifieds.Core.Models.Domains;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Classifieds.Core.ViewModels
{
    public class HomePageViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Classified> Classifieds { get; set; }
        public SortClassifieds SortClassifieds { get; set; }


    }
}
