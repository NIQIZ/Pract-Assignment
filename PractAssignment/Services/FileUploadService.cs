namespace PractAssignment.Services;

public class FileUploadService
{
    
    private readonly IWebHostEnvironment _environment;
    
    public FileUploadService(IWebHostEnvironment environment)
    {
        _environment = environment;
    }
    
    public async Task<bool> UploadFile(IFormFile file)
    {
        string path;
        try
        {
            if (file.Length > 0)
            {
                path = Path.GetFullPath(Path.Combine(_environment.WebRootPath, "uploads"));
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                using (var fileStream = new FileStream(Path.Combine(path, file.FileName), FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                return true;
            }

            return false;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}