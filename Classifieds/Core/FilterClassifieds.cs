using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Classifieds.Core
{
    public class FilterClassifieds
    {
        public string Title { get; set; }
        public int categoryId { get; set; }
        public string UserId { get; set; }
        public bool Active { get; set; }

       
    }
}
