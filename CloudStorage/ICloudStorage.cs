using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace CloudStorage
{
    public interface ICloudStorage
    {
        Task<string> UploadFileAsync(IFormFile imageFile, string fileNameForStorage);
        Task<string> UpdateFileAsync(IFormFile newImageFile, string oldImageUrl, string fileNamePrefix);
        Task DeleteFileAsync(string fileNameForStorage);
    }
}