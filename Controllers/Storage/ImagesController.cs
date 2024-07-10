using cyberforgepc.Helpers.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace cyberforgepc.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImagesController : Controller
    {
        private readonly BlobStorageService _blobStorageService;

        public ImagesController(BlobStorageService blobStorageService)
        {
            _blobStorageService = blobStorageService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage(IFormFile image)
        {
            if (image == null || image.Length == 0)
                return BadRequest("No image uploaded.");

            using var stream = image.OpenReadStream();
            var fileName = Path.GetFileName(image.FileName);
            var imageUrl = await _blobStorageService.UploadImageAsync(stream, fileName);

            return Ok(new { Url = imageUrl });
        }

        [HttpDelete("delete/{fileName}")]
        public async Task<IActionResult> DeleteImage(string fileName)
        {
            var result = await _blobStorageService.DeleteImageAsync(fileName);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpGet("download/{fileName}")]
        public async Task<IActionResult> DownloadImage(string fileName)
        {
            var stream = await _blobStorageService.DownloadImageAsync(fileName);
            if (stream == null)
                return NotFound();

            return File(stream, "image/jpeg");
        }
    }
}
