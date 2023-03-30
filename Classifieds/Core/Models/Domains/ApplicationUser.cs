using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Classifieds.Core.Models.Domains
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Classifieds = new Collection<Classified>();
        }
        public ICollection<Classified> Classifieds { get; set; }
    }
}
