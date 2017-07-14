using BCPAO.PhotoManager.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BCPAO.PhotoManager.Controllers
{
    public class PhotosController : BaseController
    {
        public PhotosController(UserManager<User> userManager, DatabaseContext context) : base(userManager, context)
        {
        }

        public IActionResult Index()
        {
            if (!HasPermission("VIEW_PHOTOS"))
            {
                return Unauthorized();
            }

            return View();
        }
    }
}