using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Classifieds.Core.Models.Domains
{
    public class Email
    {
        public int Id { get; set; }

        [Display(Name = "Tytuł:")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Treść wiadomości:")]
        public string Message { get; set; }

        [Required]
        [EmailAddress]
        public string ReceiverEmail { get; set; }

        [Required]
        [EmailAddress]
        public string SenderEmail { get; set; }
    }
}
