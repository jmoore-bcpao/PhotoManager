using BCPAO.PhotoManager.Data;
using BCPAO.PhotoManager.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace BCPAO.PhotoManager.Controllers
{
    public class SearchController : BaseController
    {
        private const int PageSize = 25;

        public SearchController(UserManager<User> userManager, DatabaseContext context) : base(userManager, context)
        {
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(SearchFilterViewModel search, int page = 1, string sort = "")
        {
            //if (ModelState.IsValid)
            //{
            var photos = _context.Photos
                .Select(p => new PhotoViewModel
                {
                    PhotoId = p.PhotoId,
                    PropertyId = p.PropertyId,
                    ImageString = string.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(p.ImageData))
                });

                if (search.PhotoId != null)
                {
                    photos = photos.Where(p => p.PhotoId == search.PhotoId);
                }
                if (search.PropertyId != null)
                {
                    photos = photos.Where(p => p.PropertyId == search.PropertyId);
                }

                int filteredCount = photos.Count();
                int totalPages = Convert.ToInt32(Math.Ceiling((decimal)filteredCount / PageSize));

                switch (sort.ToLower())
                {
                    default:
                        photos = photos.OrderBy(p => p.PropertyId);
                        break;
                    //case "namedesc":
                    //    photos = photos.OrderByDescending(u => u.FirstName).ThenByDescending(u => u.LastName);
                    //    break;
                    //case "emailasc":
                    //    photos = photos.OrderBy(u => u.Email);
                    //    break;
                    //case "emaildesc":
                    //    photos = photos.OrderByDescending(u => u.Email);
                    //    break;
                    //case "typeasc":
                    //    photos = photos.OrderBy(u => u.UsertypeSortingOrder);
                    //    break;
                    //case "typedesc":
                    //    photos = photos.OrderByDescending(u => u.UsertypeSortingOrder);
                    //    break;
                }

                photos = photos.Skip(PageSize * (page - 1)).Take(PageSize);
            
                return View(photos);
            //}

            //return View();
        }

    }
}