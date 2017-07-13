using BCPAO.PhotoManager.Data;
using BCPAO.PhotoManager.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BCPAO.PhotoManager.Controllers
{
    public class UploadController : BaseController
    {
        public UploadController(UserManager<User> userManager, DatabaseContext context) : base(userManager, context)
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        //public IActionResult Index(PhotoViewModel model)
        //{
        //    return View();
        //}
    }
}