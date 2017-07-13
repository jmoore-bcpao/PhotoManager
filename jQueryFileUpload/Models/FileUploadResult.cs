using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jQueryFileUpload.Models
{
    public class FileUploadResult
    {
        public string Name { get; set; }
        public int Size { get; set; }
        public string Type { get; set; }
        public string Url { get; set; }
        public string DeleteUrl { get; set; }
        public string ThumbnailUrl { get; set; }
        public string DeleteType { get; set; }
    }
}
