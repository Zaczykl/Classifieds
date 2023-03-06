using Classifieds.Core.Models.Domains;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Classifieds.Core.ViewModels
{
    public class CreateClassifiedViewModel
    {        
        public string FormattedPrice { get; set; }
        public Classified Classified { get; set; }
        public IFormFile File { get; set; }
        public IEnumerable<IFormFile> Files { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public bool ThumbnailChanged { get; set; }
        public bool ImagesChanged { get; set; }
        public string thumbnailUrl { get; set; }
    }
}
