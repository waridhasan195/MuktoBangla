
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace MuktoBangla.Repositories
{
    public class CloudinaryImageRepository : IImageRapository
    {
        private readonly IConfiguration configuration;
        private readonly Account account;
        public CloudinaryImageRepository(IConfiguration configuration)
        {
            this.configuration = configuration;

            account = new Account(
                configuration.GetSection("Cloudnary")["CloudName"],
                configuration.GetSection("Cloudnary")["ApiKey"],
                configuration.GetSection("Cloudnary")["ApiSecret"]
                );
        }

        public async Task<string> UploadImageAsync(IFormFile file)
        {
            var clint = new Cloudinary(account);
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                UseFilename = true,
                UniqueFilename = false,
                Overwrite = true,
                DisplayName=file.Name
            };

            var uploadResult = await clint.UploadAsync(uploadParams);

            if(uploadResult != null && uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return uploadResult.SecureUri.ToString();
            }
            return null;
        }
    }
}
