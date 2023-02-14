using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Classifieds.Core.Models.Domains
{
    public class Classified
    {
        private const int _titleMinLength = 5;
        private const int _titleMaxLength = 50;
        private const int _descriptionMinLength = 15;
        private const int _descriptionMaxLength = 2000;
        public Classified()
        {
            ProductImages = new Collection<ProductImage>();
        }

        public int Id { get; set; }


        [Required]
        [MinLength(_titleMinLength)]
        [MaxLength(_titleMaxLength)]
        [Display(Name = "Tytuł")]
        public string Title { get; set; }


        [Required]
        [MinLength(_descriptionMinLength)]
        [MaxLength(_descriptionMaxLength)]
        [Display(Name = "Opis")]
        public string Description { get; set; }


        [Display(Name = "Cena")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }


        [Display(Name = "Kategoria")]
        [Required]
        public int? CategoryId { get; set; }

        public string UserId { get; set; }


        public ApplicationUser User { get; set; }


        public Category Category { get; set; }

        public ICollection<ProductImage> ProductImages { get; set; }

        public int DescriptionMinLength
        {
            get { return _descriptionMinLength; }
        }
        public int DescriptionMaxLength
        {
            get { return _descriptionMaxLength; }
        }
        public int TitleMinLength
        {
            get { return _titleMinLength; }
        }
        public int TitleMaxLength
        {
            get { return _titleMaxLength; }
        }
    }
}
