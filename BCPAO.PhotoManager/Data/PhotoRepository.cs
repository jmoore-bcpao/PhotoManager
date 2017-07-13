using BCPAO.PhotoManager.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System;

namespace BCPAO.PhotoManager.Data
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly DatabaseContext _context;

        private readonly ILogger _logger;

        public PhotoRepository(DatabaseContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("PhotoRepository");
        }

        //public IEnumerable<PhotoViewModel> AddPhoto(FileResult fileResult)
        //{
        //    throw new NotImplementedException();
        //}

        public void AddPhoto(PhotoViewModel model)
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

        //public IEnumerable<PhotoViewModel> GetAllPhotos(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public FileDescription GetFileDescription(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //private IEnumerable<PhotoViewModel> GetNewFiles(List<string> fileNames)
        //{
        //    //var x = _context.Photos.Where(r => fileNames.Contains(r.ImageName));
        //    //return x.Select(t => new PhotoViewModel { PhotoId = t.PhotoId });
        //}

        //public IEnumerable<FileDescriptionShort> GetAllFiles()
        //{
        //    return _context.FileDescriptions.Select(
        //            t => new FileDescriptionShort { Name = t.FileName, Id = t.Id, Description = t.Description });
        //}

        //public FileDescription GetFileDescription(int id)
        //{
        //    return _context.FileDescriptions.Single(t => t.Id == id);
        //}
    }
}
