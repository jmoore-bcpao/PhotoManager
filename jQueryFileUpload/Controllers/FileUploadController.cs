using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace jQueryFileUpload.Controllers
{
    public class FileUploadController : Controller
    {
        private readonly IHostingEnvironment _env;

        //string tempPath = "~/somefiles/";
        //string serverMapPath = "~/Files/somefiles/";
        //string UrlBase = "/Files/somefiles/";
        //string DeleteURL = "/FileUpload/DeleteFile/?file=";
        //string DeleteType = "GET";
        
        public FileUploadController(IHostingEnvironment env)
        {
            _env = env;
        }

        public string StorageRoot => Path.Combine(_env.WebRootPath);

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Show()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetFileList()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Upload()
        {
            return View();
        }

        [HttpGet]
        public IActionResult DeleteFile(string file)
        {
            return View();
        }

    }
}
