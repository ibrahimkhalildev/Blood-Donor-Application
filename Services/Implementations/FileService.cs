using BloodDonar.MVC.Services.Interfaces;
using System.Drawing;
namespace BloodDonar.MVC.Services.Implementations
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _env;
        public FileService(IWebHostEnvironment env)
        {
            _env = env;
        }
        public async Task<string> SaveFileAsync(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                var folderPath = Path.Combine(_env.WebRootPath, "profiles");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath); // Ensure the directory exists
                }
                var fullPath = Path.Combine(folderPath, fileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                return Path.Combine("profiles", fileName); // Set the path to the saved image
            }
            //if (donor.ProfilePicture != null && donor.ProfilePicture.Length > 0)
            //{
            //    // Save the profile picture to a specific location and set the path
            //    var fileName = Path.GetFileName(donor.ProfilePicture.FileName);
            //    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);
            //    using (var stream = new FileStream(filePath, FileMode.Create))
            //    {
            //        donor.ProfilePicture.CopyTo(stream);
            //    }
            //    donorEntty.ProfilePicture = $"/images/{fileName}"; // Set the path to the saved image
            //}
            return string.Empty; // Return null if no file was provided or if the file is empty
        }

        public Task<string> UploadFileAsync(IFormFile file)
        {
            throw new NotImplementedException();
        }
    }
}
