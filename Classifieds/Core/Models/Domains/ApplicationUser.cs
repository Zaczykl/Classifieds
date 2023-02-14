using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Classification;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

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
