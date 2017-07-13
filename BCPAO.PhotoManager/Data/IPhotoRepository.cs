using BCPAO.PhotoManager.Models;
using System.Collections.Generic;
using System.IO;

namespace BCPAO.PhotoManager.Data
{ 
    public interface IPhotoRepository
    {
        void AddPhoto(PhotoViewModel model);

        //IEnumerable<PhotoViewModel> AddPhoto(FileResult fileResult);
        //IEnumerable<PhotoViewModel> GetAllPhotos(int id);
        //IEnumerable<Photo> GetPhotosByAlbum(string owner, string albumId);
        //Photo GetPhotoByOwner(string owner, string albumId, string photoId);
        //void AddPhoto(Photo photo, Stream binary, string mimeType, string name);
        //void DeletePhoto(string owner, string album, string photoId);
        //void UpdatePhotoData(Photo photo);
    }
}
