namespace Classifieds.Core.Models.Domains
{
    public class ProductImage
    {
        public int Id { get; set; }
        public byte[] Image { get; set; }
        public int ClassifiedId { get; set; }
        public string UserId { get; set; }
        public bool Thumbnail { get; set; }
        public Classified Classified { get; set; }
    }
}
