using TheOpenJournal.Services.Interfaces;

namespace TheOpenJournal.Services.Implementation
{
    public class UtilityService:IUtilityService
    {
        public IWebHostEnvironment _environment;
        public UtilityService(IWebHostEnvironment environment)
        {
            _environment = environment;            
        }

        public async Task<string> SaveImage(IFormFile file)
        {
            //check if file is properly passed or not.
            if (file == null || file.Length == 0) {
                throw new ArgumentException("File is null or empty !");
            }
            try
            {
                //create a folderpath using WebRootPath
                string folderPath = Path.Combine(_environment.WebRootPath, "blogImages");
                //if filder doesnot exists, then create one.
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                //As name can be same, Generate Guid, and add with filename
                string fileName = Guid.NewGuid().ToString()+Path.GetExtension(file.FileName);
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
                return $"uploads/{fileName}";
            }catch(Exception ex)
            {
                throw new Exception($"Error saving file.{ex.Message}");
            }
        }
    }
}
