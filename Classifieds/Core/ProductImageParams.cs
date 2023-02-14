using Classifieds.Core.Models.Domains;
using Microsoft.AspNetCore.Http;

namespace Classifieds.Core
{
    public class ProductImageParams
    {
        public IFormFile File { get; set; }
        public Classified Classified { get; set; }
        public string UserId { get; set; }
        public bool Thumbnail { get; set; }
    }
}
