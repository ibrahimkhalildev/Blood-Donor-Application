namespace BloodDonar.MVC.Services.Interfaces
{
    public interface IFileService
    {
        Task<string> SaveFileAsync(IFormFile? profilePicture);
        Task<string> UploadFileAsync(IFormFile file);
    }
}
