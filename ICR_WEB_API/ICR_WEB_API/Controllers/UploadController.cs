using ICR_WEB_API.Service.BLL.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

namespace ICR_WEB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UploadController(IWebHostEnvironment env, IResponseRepo responseRepo) : ControllerBase
    {
        private readonly IWebHostEnvironment _env = env;
        private readonly IResponseRepo _responseRepo = responseRepo;

        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile file, string unifiedLicenseNumber)
        {
            if (file == null || file.Length == 0)
                return BadRequest(new
                {
                    Message = "No file uploaded"
                });

            if (!IsValidImage(file))
            {
                return BadRequest(new
                {
                    Message = "Image is not a valid format"
                });
            }

            var isExist = await _responseRepo.IsExist(unifiedLicenseNumber);

            if (!isExist)
            {
                return NotFound(new
                {
                    Message = "Response not found"
                });
            }

            var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            //var extension = Path.GetExtension(file.FileName);
            var extension = ".jpg";
            var uniqueFileName = $"{unifiedLicenseNumber}{extension}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var image = await Image.LoadAsync(file.OpenReadStream()))
            {
                int maxWidth = 1024;
                if (image.Width > maxWidth)
                {
                    image.Mutate(x => x.Resize(maxWidth, 0)); // 0 height for maintains the aspect ratio.
                }

                var encoder = new JpegEncoder { Quality = 75 };
                await image.SaveAsync(filePath, encoder);
            }

            var fileUrl = $"/uploads/{uniqueFileName}";

            return Ok(new { fileUrl });
        }

        private static bool IsValidImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return false;

            var allowedMimeTypes = new[] { "image/jpeg", "image/png", "image/gif", "image/bmp" };
            if (!allowedMimeTypes.Contains(file.ContentType.ToLower()))
                return false;

            // Read the first few bytes (header) of the file to verify the file
            byte[] headerBytes = new byte[8];
            using (var stream = file.OpenReadStream())
            {
                if (stream.Length < headerBytes.Length)
                    return false;

                stream.Read(headerBytes, 0, headerBytes.Length);
            }

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (extension == ".jpeg" || extension == ".jpg")
            {
                // JPEG files start with FF D8.
                return headerBytes[0] == 0xFF && headerBytes[1] == 0xD8;
            }
            else if (extension == ".png")
            {
                // PNG signature: 89 50 4E 47 0D 0A 1A 0A.
                byte[] pngSignature = new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A };
                return pngSignature.SequenceEqual(headerBytes);
            }
            else if (extension == ".gif")
            {
                // GIF signature: first 6 bytes should be "GIF87a" or "GIF89a".
                string headerString = System.Text.Encoding.ASCII.GetString(headerBytes.Take(6).ToArray());
                return headerString == "GIF87a" || headerString == "GIF89a";
            }
            else if (extension == ".bmp")
            {
                // BMP files start with "BM"
                string headerString = System.Text.Encoding.ASCII.GetString(headerBytes.Take(2).ToArray());
                return headerString == "BM";
            }

            return false;
        }

    }
}
