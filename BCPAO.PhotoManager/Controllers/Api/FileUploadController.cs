using BCPAO.PhotoManager.Data;
using BCPAO.PhotoManager.Filters;
using BCPAO.PhotoManager.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BCPAO.PhotoManager.Controllers
{
    [Produces("application/json")]
    [Route("api/FileUpload")]
    public class FileUploadController : Controller
    {
        private readonly IPhotoRepository _photoRepository;
        private readonly IOptions<ApplicationConfiguration> _appConfig;
        private readonly IHostingEnvironment _environment;
        private readonly UserManager<User> _userManager;
        private readonly DatabaseContext _context;

        public FileUploadController(
            UserManager<User> userManager, 
            DatabaseContext context,
            IPhotoRepository photoRepository,
            IOptions<ApplicationConfiguration> appConfig,
            IHostingEnvironment environment)
            {
                _userManager = userManager;
                _context = context;
                _photoRepository = photoRepository;
                _appConfig = appConfig;
                _environment = environment;
            }

        [Route("")]
        [HttpGet]
        public IActionResult Get()
        {
           return Ok();
        }

        [Route("")]
        [HttpPost]
        [ServiceFilter(typeof(ValidateMimeMultipartContentFilter))]
        public async Task<IActionResult> Post(ICollection<IFormFile> files)
        {
            var response = new FileResponse();
            
            if (ModelState.IsValid)
            {
                var uploads = Path.Combine(@"C:\dev\BCPAO.PhotoManager\BCPAO.PhotoManager\", "Uploads");

                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                        using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                        {
                            using (var memoryStream = new MemoryStream())
                            {
                                await file.CopyToAsync(memoryStream);

                                var photo = new PhotoViewModel
                                {
                                    PropertyId = 1234567,
                                    BuildingId = 1,
                                    BuildingSeq = 1,
                                    ImageData = memoryStream.ToArray(),
                                    ImageName = fileName,
                                    ImageSize = file.Length / 1024M,
                                    DateTaken = DateTime.UtcNow,
                                    UploadedDate = DateTime.UtcNow,
                                    UploadedBy = User.Identity.Name.ToLowerInvariant(),
                                    MasterPhoto = false,
                                    FrontPhoto = false,
                                    PublicPhoto = false,
                                    Status = "Pending",
                                    Active = true
                                };

                                _photoRepository.Add(photo);
                            }
                        }

                    }
                }
            }







            //response = new FileResponse
            //{
            //    Files = new List<Photo>
            //    {
            //        new Photo {
            //            Name = fileName,
            //            Size = file.Length,
            //            Url = "",
            //            ThumbnailUrl = "",
            //            DeleteUrl = "",
            //            DeleteType = "DELETE"
            //        }
            //    }
            //};

            return Ok(response);

            //var files = new FileResult
            //{
            //    FileNames = names,
            //    ContentTypes = contentTypes,
            //    Description = fileDescriptionShort.Description,
            //    CreatedTimestamp = DateTime.UtcNow,
            //    UpdatedTimestamp = DateTime.UtcNow,
            //};

            //_fileRepository.AddFileDescriptions(files);

            //return RedirectToAction("ViewAllFiles", "FileClient");

        }


        //[Route("download/{id}")]
        //[HttpGet]
        //public FileStreamResult Download(int id)
        //{
        //    var fileDescription = _fileRepository.GetFileDescription(id);

        //    var path = _appConfig.Value.ServerUploadFolder + "\\" + fileDescription.FileName;
        //    var stream = new FileStream(path, FileMode.Open);
        //    return File(stream, fileDescription.ContentType);
        //}



        //// PUT: api/FileUpload/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
