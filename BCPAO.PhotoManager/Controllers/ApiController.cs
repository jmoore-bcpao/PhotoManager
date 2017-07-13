using BCPAO.PhotoManager.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BCPAO.PhotoManager
{
    [Produces("application/json")]
    public class ApiController : Controller
    {
        private DatabaseContext _context;

        public ApiController(DatabaseContext context)
        {
            _context = context;
        }

        [Route("api/settings")]
        [HttpGet]
        public Setting GetSetting(string name)
        {
            return _context.Settings.FirstOrDefault(s => s.Name == name);
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}