using BCPAO.PhotoManager.Data;
using BCPAO.PhotoManager.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BCPAO.PhotoManager.Controllers
{
    public class TagController : BaseController
    {
        //private readonly IPhotoRepository _photoRepository;
        //private readonly IOptions<ApplicationConfiguration> _appConfig;
        //private readonly IHostingEnvironment _environment;
        //private readonly UserManager<User> _userManager;
        //private readonly DatabaseContext _context;

        public TagController(UserManager<User> userManager, DatabaseContext context) : base(userManager, context)
        {
        }

        public async Task<IActionResult> Index()
        {
            var photos = await _context.Photos
                .Select(m => new PhotoViewModel
                {
                    PhotoId = m.PhotoId,
                    PropertyId = m.PropertyId,
                    BuildingId = m.BuildingId,
                    BuildingSeq = m.BuildingSeq,
                    MasterPhoto = m.MasterPhoto,
                    FrontPhoto = m.FrontPhoto,
                    PublicPhoto = m.PublicPhoto,
                    Status = m.Status,
                    UploadedBy = m.UploadedBy,
                    UploadedDate = m.UploadedDate,
                    DateTaken = m.DateTaken,
                    Active = m.Active,
                    UserId = m.UserId,
                    ImageString = string.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(m.ImageData))
                }).Take(10).Skip(0).ToListAsync();

            if (photos == null)
            {
                return NotFound();
            }

            return View(photos);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(int id, [Bind("PhotoId,PropertyId,BuildingId,BuildingSeq,ImageData,ImageName,ImageSize,DateTaken,UploadedDate,UploadedBy,UserId,MasterPhoto,FrontPhoto,PublicPhoto,Status,Active")] Photo photo)
        {
            if (id != photo.PhotoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(photo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhotoExists(photo.PhotoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return PartialView(photo);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var photo = await _context.Photos
                .Select(m => new PhotoViewModel
                {
                    PhotoId = m.PhotoId,
                    PropertyId = m.PropertyId,
                    BuildingId = m.BuildingId,
                    BuildingSeq = m.BuildingSeq,
                    MasterPhoto = m.MasterPhoto,
                    FrontPhoto = m.FrontPhoto,
                    PublicPhoto = m.PublicPhoto,
                    Status = m.Status,
                    UploadedBy = m.UploadedBy,
                    UploadedDate = m.UploadedDate,
                    DateTaken = m.DateTaken,
                    Active = m.Active,
                    UserId = m.UserId,
                    ImageString = string.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(m.ImageData))
                }).SingleOrDefaultAsync(m => m.PhotoId == id);

            if (photo == null)
            {
                return NotFound();
            }

            return PartialView("DetailsPartial", photo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PostModal()
        {
            var closeModal = new CloseModal
            {
                ShouldClose = true,
                FetchData = false
            };

            return PartialView("CloseModal", closeModal);
        }




        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var photo = await _context.Photos
                .Select(m => new PhotoViewModel
                {
                    PhotoId = m.PhotoId,
                    PropertyId = m.PropertyId,
                    BuildingId = m.BuildingId,
                    BuildingSeq = m.BuildingSeq,
                    MasterPhoto = m.MasterPhoto,
                    FrontPhoto = m.FrontPhoto,
                    PublicPhoto = m.PublicPhoto,
                    Status = m.Status,
                    UploadedBy = m.UploadedBy,
                    UploadedDate = m.UploadedDate,
                    DateTaken = m.DateTaken,
                    Active = m.Active,
                    UserId = m.UserId,
                    ImageString = string.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(m.ImageData))  // convert back to binary
                }).SingleOrDefaultAsync(m => m.PhotoId == id);

            if (photo == null)
            {
                return NotFound();
            }

            return View(photo);
        }
        
        // To protect from overposting attacks, please enable the specific properties you want to bind to, 
        // for more details see http://go.microsoft.com/fwlink/?LinkId=317598
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PhotoId,PropertyId,BuildingId,BuildingSeq,ImageData,ImageName,ImageSize,DateTaken,UploadedDate,UploadedBy,UserId,MasterPhoto,FrontPhoto,PublicPhoto,Status,Active")] Photo photo)
        {
            if (id != photo.PhotoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(photo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhotoExists(photo.PhotoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return PartialView(photo);
        }




        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var photo = await _context.Photos.SingleOrDefaultAsync(m => m.PhotoId == id);
            if (photo == null)
            {
                return NotFound();
            }

            return PartialView("DeletePartial", photo);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var photo = await _context.Photos.SingleOrDefaultAsync(m => m.PhotoId == id);
            _context.Photos.Remove(photo);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        private bool PhotoExists(int id)
        {
            return _context.Photos.Any(e => e.PhotoId == id);
        }

    }
}