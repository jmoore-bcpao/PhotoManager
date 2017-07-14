using BCPAO.PhotoManager.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace BCPAO.PhotoManager.Controllers
{
    [Route("api/v1/image")]
    public class ImageController : BaseController
    {
        public ImageController(UserManager<User> userManager, DatabaseContext context) : base(userManager, context)
        {
        }

        [Route("")]
        [Route("{account:int}")]
        public IActionResult Get(int account = 2106510, int w = 640, int h = 480)
        {
            try
            {
                int width = w > 0 ? w : 640;
                int height = h > 0 ? h : 480;

                var photos = _context.Photos.Where(p => p.PropertyId == account).ToList();
                if (photos == null)
                {
                    //var noPhoto = HttpContext.Current.Server.MapPath("~/img/placeholder_image.jpg");

                    //var noImage = ImageHelper.GetResizedImage(noPhoto, width, height);
                    //response.Content = new ByteArrayContent(noImage);
                    //response.Content.Headers.ContentType = ImageHelper.GetEncodingName(noPhoto);

                    //return ResponseMessage(response);
                }

                //var imageData = ImageHelper.GetResizedImage(photos[0].ImageData, width, height);
                byte[] imageData = null;

                var response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new ByteArrayContent(imageData);
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpg");

                return Ok(response);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
