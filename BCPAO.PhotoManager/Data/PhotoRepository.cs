using BCPAO.PhotoManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BCPAO.PhotoManager.Data
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly DatabaseContext _context;

        //private readonly ILogger _logger;

        public PhotoRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Photo>> GetPhotos(int propertyId)
        {
            return await _context.Photos
              .Where(p => p.PropertyId == propertyId)
              .ToListAsync();
        }

        public void Add(PhotoViewModel model)
        {
            try
            {
                var photo = new Photo
                {
                    PropertyId = model.PropertyId,
                    BuildingId = model.BuildingId,
                    BuildingSeq = model.BuildingSeq,
                    ImageData = model.ImageData,
                    ImageName = model.ImageName,
                    ImageSize = model.ImageSize,
                    DateTaken = model.DateTaken,
                    UploadedDate = model.UploadedDate,
                    UploadedBy = model.UploadedBy,
                    MasterPhoto = model.MasterPhoto,
                    FrontPhoto = model.FrontPhoto,
                    PublicPhoto = model.PublicPhoto,
                    Status = model.Status, // Active, Archived, Deleted, Incomplete, Processing
                    Active = model.Active
                };

                _context.Photos.Add(photo);

                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

           // return GetNewFiles(fileNames);
        }

        public void Remove(PhotoViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
