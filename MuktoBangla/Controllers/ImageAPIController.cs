using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MuktoBangla.Repositories;
using System.Net;

namespace MuktoBangla.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageAPIController : ControllerBase
    {
        private readonly IImageRapository imageRapository;

        public ImageAPIController(IImageRapository imageRapository)
        {
            this.imageRapository = imageRapository;
        }
        public async Task<IActionResult> UploadImageApiAsync(IFormFile file)
        {
            var imageUrl = await imageRapository.UploadImageAsync(file);
            if (imageUrl == null)
            {
                return Problem("Something went WRONG! ", null, (int)HttpStatusCode.InternalServerError);
            }
            return new JsonResult(new { link = imageUrl });
        }
    }
}
