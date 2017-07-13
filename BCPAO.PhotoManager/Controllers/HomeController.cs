using BCPAO.PhotoManager.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BCPAO.PhotoManager
{
    [Authorize]
    public class HomeController : BaseController
    {
        public HomeController(UserManager<User> userManager, DatabaseContext context) : base(userManager, context)
        {
        }

        //public IActionResult Index()
        //{
        //    if (!HasPermission("VIEW_PHOTOS"))
        //    {
        //        return Unauthorized();
        //    }

        //    return View();
        //}

        //[Authorize(Policy = "HasPermission")]
        public IActionResult Index()
        {
            ViewData["activePhotosToday"] = 0;
            ViewData["activePhotosWeek"] = 0;
            ViewData["activePhotosMonth"] = 0;
            ViewData["activePhotosYear"] = 0;
            ViewData["activePhotosTotal"] = 0;

            ViewData["archivedPhotosToday"] = 0;
            ViewData["archivedPhotosWeek"] = 0;
            ViewData["archivedPhotosMonth"] = 0;
            ViewData["archivedPhotosYear"] = 0;
            ViewData["archivedPhotosTotal"] = 0;

            //var countCustomers = _context.Customers.Count();
            //ViewData["countCustomers"] = countCustomers;

            //var countBookings = _context.Bookings.Count();
            //ViewData["countBookings"] = countBookings;

            //var countBookingsToday = _context.Bookings.Where(b => b.CreatedAt.Date.Equals(DateTime.Now.Date)).Count();
            //ViewData["countBookingsToday"] = countBookingsToday;

            //var startOfWeek = DateTime.Now.Date;
            //while (startOfWeek.DayOfWeek != DayOfWeek.Sunday)
            //{
            //    startOfWeek = startOfWeek.AddDays(-1);
            //}
            //var endOfWeek = startOfWeek.AddDays(7);

            //var countBookingsThisWeek = _context.Bookings.Where(b => b.CreatedAt.Date >= startOfWeek && b.CreatedAt.Date <= endOfWeek).Count(); ;
            //ViewData["countBookingsThisWeek"] = countBookingsThisWeek;

            //var countPackagesPendingPickup = _context.Packages.Where(p => p.Status == PackageStatus.PendingPickup).Count();
            //ViewData["countPackagesPendingPickup"] = countPackagesPendingPickup;

            //var countPackagesDeliveredToday = _context.Packages.Where(p => p.DeliveredAt.GetValueOrDefault().Date.Equals(DateTime.Now.Date)).Count();
            //ViewData["countPackagesDeliveredToday"] = countPackagesDeliveredToday;

            //decimal incomeToday = 0;

            //var paymentsToday = _context.Payments.Where(p => p.ProcessedAt.Date.Equals(DateTime.Now.Date));

            //foreach (var payment in paymentsToday)
            //{
            //    incomeToday += payment.Amount;
            //}

            //ViewData["incomeToday"] = incomeToday;

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
