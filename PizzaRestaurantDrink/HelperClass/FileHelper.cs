using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace PizzaRestaurantDrink.HelperClass
{
    public interface IFileHelper
    {
        bool UploadPhoto(IFormFile file, string folder, string name);
    }

    public class FileHelper : IFileHelper
    {
        private readonly IWebHostEnvironment _env;

        public FileHelper(IWebHostEnvironment env)
        {
            _env = env;
        }

        public bool UploadPhoto(IFormFile file, string folder, string name)
        {
            if (file == null || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(folder))
            {
                return false;
            }
            try
            {
                string webRootPath = _env.WebRootPath;
                string path = Path.Combine(webRootPath, folder);

                // FIX: Create directory if it does not exist
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                string filePath = Path.Combine(path, name);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}