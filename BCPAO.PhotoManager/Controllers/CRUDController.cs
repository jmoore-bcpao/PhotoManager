using BCPAO.PhotoManager.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BCPAO.PhotoManager.Controllers
{
    public class CRUDController : Controller
    {
        private readonly DatabaseContext _context;

        public CRUDController(DatabaseContext context)
        {
            _context = context;    
        }

        // GET: CRUD
        public async Task<IActionResult> Index()
        {
            return View(await _context.Photos.ToListAsync());
        }

        // GET: CRUD/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var photo = await _context.Photos
                .SingleOrDefaultAsync(m => m.PhotoId == id);
            if (photo == null)
            {
                return NotFound();
            }

            return View(photo);
        }

        // GET: CRUD/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CRUD/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PhotoId,PropertyId,BuildingId,BuildingSeq,ImageData,ImageName,ImageSize,DateTaken,UploadedDate,UploadedBy,UserId,MasterPhoto,FrontPhoto,PublicPhoto,Status,Active")] Photo photo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(photo);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(photo);
        }

        // GET: CRUD/Edit/5
        public async Task<IActionResult> Edit(int? id)
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
            return View(photo);
        }

        // POST: CRUD/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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
            return View(photo);
        }

        // GET: CRUD/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var photo = await _context.Photos
                .SingleOrDefaultAsync(m => m.PhotoId == id);
            if (photo == null)
            {
                return NotFound();
            }

            return View(photo);
        }

        // POST: CRUD/Delete/5
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
