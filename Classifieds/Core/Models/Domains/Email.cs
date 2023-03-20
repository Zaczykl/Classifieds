using System.ComponentModel.DataAnnotations;

namespace Classifieds.Core.Models.Domains
{
    public class Email
    {
        private const int _messageMaxLength = 150;
        private const int _titleMaxLength = 50;
        public int Id { get; set; }

        [Display(Name = "Tytuł:")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Treść wiadomości:")]
        [MaxLength(_messageMaxLength)]
        public string Message { get; set; }

        [Required]
        [EmailAddress]
        public string ReceiverEmail { get; set; }

        [Required]
        [EmailAddress]
        public string SenderEmail { get; set; }

        public int ClassifiedId { get; set; }

        public int MessageMaxLength
        {
            get { return _messageMaxLength; }
        }
        public int TitleMaxLength
        {
            get { return _titleMaxLength; }
        }
    }
}
