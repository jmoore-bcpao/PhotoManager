using BCPAO.PhotoManager.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BCPAO.PhotoManager.Controllers
{
    public class ReportsController : BaseController
    {
        public ReportsController(UserManager<User> userManager, DatabaseContext context) : base(userManager, context)
        {
        }

        public IActionResult Index()
        {
            //if (!HasPermission("VIEW_REPORTS"))
            //{
            //    return Unauthorized();
            //}

            return View();
        }
    }
}
