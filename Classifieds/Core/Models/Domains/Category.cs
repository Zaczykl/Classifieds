using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Classifieds.Core.Models.Domains
{
    public class Category
    {
        public Category()
        {
            Classifieds = new Collection<Classified>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Classified> Classifieds { get; set; }
    }
}
