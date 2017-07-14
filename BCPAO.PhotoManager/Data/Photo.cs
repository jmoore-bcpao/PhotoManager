using System;

namespace BCPAO.PhotoManager.Data
{
    public class Photo
    {
        public int PhotoId { get; set; }
        public int? PropertyId { get; set; }
        public int? BuildingId { get; set; }
        public int? BuildingSeq { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageName { get; set; }
        public decimal ImageSize { get; set; }
        public DateTime? DateTaken { get; set; }
        public DateTime UploadedDate { get; set; }
        public string UploadedBy { get; set; }
        public bool? MasterPhoto { get; set; }
        public bool? FrontPhoto { get; set; }
        public bool? PublicPhoto { get; set; }
        public string Status { get; set; }
        public bool? Active { get; set; }
    }
}
