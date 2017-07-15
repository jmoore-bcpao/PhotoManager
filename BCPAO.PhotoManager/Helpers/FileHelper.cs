using Microsoft.AspNetCore.Http;
using System.IO;

namespace BCPAO.PhotoManager.Helpers
{
    public class FileHelper
    {
        public static byte[] ConvertToBytes(IFormFile file)
        {
            var stream = file.OpenReadStream();
            using (var ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}
