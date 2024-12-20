using TheOpenJournal.Services.Interfaces;

namespace TheOpenJournal.Services.Implementation
{
    public class UtilityService:IUtilityService
    {
        public IWebHostEnvironment _environment;
        public IHttpContextAccessor _httpContextAccessor;
        public UtilityService(IWebHostEnvironment environment, IHttpContextAccessor httpContextAccessor)
        {
            _environment = environment;
            _httpContextAccessor = httpContextAccessor;
        }
        
        public async Task<string> SaveImageAsync(IFormFile file)
        {
            //check if file is properly passed or not.
            if (file == null || file.Length == 0) {
                throw new ArgumentException("File is null or empty !");
            }
            try
            {
                //Allowed Extension
                var allowedExtension = new[] { ".jpg", ".png", ".jpeg"};
                //Get file extension and convert to lowercase.
                string fileExtension = Path.GetExtension(file.FileName).ToLower();

                // check if file is valid or not
                if (!allowedExtension.Contains(fileExtension)) {
                    throw new ArgumentException("Not valid file");
                }

                //create a folderpath using WebRootPath
                string folderPath = Path.Combine(_environment.ContentRootPath,"wwwroot", "blogImages");
                //if filder doesnot exists, then create one.
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                //As name can be same for different images, Generate Guid, and add with filename
                string fileName = $"{Guid.NewGuid()}{fileExtension}";

                //finally combine path and filename
                string filePath = Path.Combine(folderPath, fileName);
                //use scoped stream for efficient memory management.
                //if file is exisist then it will overrite it.
                using(var stream = new FileStream(filePath, FileMode.Create))
                {
                    //finally copy the file to folder.
                    await file.CopyToAsync(stream);
                }
                //return filename
                string url = $"blogImages/{fileName}";
                return url;
            }catch(Exception ex)
            {
                throw new Exception($"Error saving file.{ex.Message}");
            }
        }
    }
}
