using BCPAO.PhotoManager.Data;
using BCPAO.PhotoManager.Filters;
using BCPAO.PhotoManager.Helpers;
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
        private readonly IPhotoRepository _repo;
        private readonly IOptions<ApplicationConfiguration> _config;
        private readonly IHostingEnvironment _env;
        private readonly UserManager<User> _userManager;
        private readonly DatabaseContext _context;

        public FileUploadController(
            UserManager<User> userManager, 
            DatabaseContext context,
            IPhotoRepository repo,
            IOptions<ApplicationConfiguration> config,
            IHostingEnvironment env)
            {
                _userManager = userManager;
                _context = context;
                _repo = repo;
                _config = config;
                _env = env;
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
                var uploads = Path.Combine(_env.ContentRootPath, "Uploads");

                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                        using (var fs = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                        {
                            using (var ms = new MemoryStream())
                            {
                                await file.CopyToAsync(ms);

                                var photo = new PhotoViewModel
                                {
                                    PropertyId = 0,
                                    BuildingId = 1,
                                    BuildingSeq = 1,
                                    ImageData = ms.ToArray(),
                                    ImageName = fileName,
                                    ImageSize = ByteSize.FromBytes(file.Length).KiloBytes,
                                    DateTaken = DateTime.UtcNow,
                                    UploadedDate = DateTime.UtcNow,
                                    UserId = Convert.ToInt32(_userManager.GetUserId(User)),
                                    UploadedBy = _userManager.GetUserName(User),
                                    MasterPhoto = false,
                                    FrontPhoto = false,
                                    PublicPhoto = false,
                                    Status = "Pending",
                                    Active = true
                                };

                                _repo.Add(photo);
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
