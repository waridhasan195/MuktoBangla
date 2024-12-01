namespace MuktoBangla.Repositories
{
    public interface IImageRapository
    {
        Task<string> UploadImageAsync(IFormFile file);
    }
}
