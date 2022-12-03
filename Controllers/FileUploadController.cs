using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace WebAPIAppFaezeh.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileUploadController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpPost("[action]")]
        public IActionResult UploadFiles(List<IFormFile> files)
        {
            if (files.Count == 0)
                return BadRequest();

            string directoryPath = Path.Combine(_webHostEnvironment.ContentRootPath, "UploadedFiles");

            foreach (var file in files)
            {
                string filePath=Path.Combine(directoryPath, file.FileName);
                using(var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            }
            return Ok("Upload successfull!");
        }
    }
}
