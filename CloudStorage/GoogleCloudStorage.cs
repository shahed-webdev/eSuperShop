using System;
using System.IO;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace CloudStorage
{
    public class GoogleCloudStorage : ICloudStorage
    {
        private readonly StorageClient _storageClient;
        private readonly string _bucketName;

        public GoogleCloudStorage(IConfiguration configuration)
        {
            var googleCredential = GoogleCredential.FromFile(configuration.GetValue<string>("GoogleCredentialFile"));
            _storageClient = StorageClient.Create(googleCredential);
            _bucketName = configuration.GetValue<string>("GoogleCloudStorageBucket");
        }

        //upload new image
        public async Task<string> UploadFileAsync(IFormFile imageFile, string fileNameForStorage)
        {
            await using var memoryStream = new MemoryStream();
            await imageFile.CopyToAsync(memoryStream);

            var dataObject = await _storageClient.UploadObjectAsync(_bucketName, fileNameForStorage, imageFile.ContentType, memoryStream);
            return dataObject.MediaLink;
        }

        //update image
        public async Task<string> UpdateFileAsync(IFormFile newImageFile, string oldImageUrl, string fileNamePrefix)
        {
            if (newImageFile == null) return oldImageUrl;

            if (!string.IsNullOrEmpty(oldImageUrl))
                await DeleteFileAsync(FileBuilder.FileNameFromUrl(oldImageUrl));

            var fileName = FileBuilder.FileNameImage(fileNamePrefix, newImageFile.FileName);

            return await UploadFileAsync(newImageFile, fileName);
        }

        //delete image
        public async Task DeleteFileAsync(string fileNameForStorage)
        {
            try
            {
                await _storageClient.DeleteObjectAsync(_bucketName, fileNameForStorage);
            }
            catch
            {
                // ignored
            }
        }
    }
}