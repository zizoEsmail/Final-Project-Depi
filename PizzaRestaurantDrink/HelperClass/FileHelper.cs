namespace PizzaRestaurantDrink.HelperClass
{
    // Interface to allow Injection
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
                // In Core, we point to the wwwroot folder
                string webRootPath = _env.WebRootPath;

                // Combine wwwroot + folder + filename
                string path = Path.Combine(webRootPath, folder, name);

                using (var stream = new FileStream(path, FileMode.Create))
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