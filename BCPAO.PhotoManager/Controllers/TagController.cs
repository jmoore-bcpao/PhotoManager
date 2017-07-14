using BCPAO.PhotoManager.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BCPAO.PhotoManager.Controllers
{
    public class TagController : BaseController
    {
        public TagController(UserManager<User> userManager, DatabaseContext context) : base(userManager, context)
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}