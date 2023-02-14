using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace Classifieds.Persistence.Extensions
{
    public static class FormFileExtension
    {
        public static async Task<byte[]> GetBytesAsync(this IFormFile formFile)
        {
            await using var memoryStream = new MemoryStream();
            await formFile.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }
    }
}
