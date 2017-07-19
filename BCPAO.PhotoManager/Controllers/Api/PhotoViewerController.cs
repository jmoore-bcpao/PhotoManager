using System.Linq;
using BCPAO.PhotoManager.Data;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BCPAO.PhotoManager.Controllers
{
    [Produces("application/json")]
    [Route("api/PhotoViewer")]
    public class PhotoViewerController : Controller
    {
        private readonly DatabaseContext _context;

        public PhotoViewerController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/PhotoViewer
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(int id)
        {
            var photos = await _context.Photos.Where(p => p.PropertyId == id).ToListAsync();

            return Ok(photos);
        }

        //// GET: api/PhotoViewer/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value";
        //}
        
        // POST: api/PhotoViewer
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/PhotoViewer/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
