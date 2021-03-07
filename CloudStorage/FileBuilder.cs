using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace CloudStorage
{
    public static class FileBuilder
    {
        public static string FileNameImage(string title, string fileName)
        {
            var fileExtension = Path.GetExtension(fileName);
            var fileNameForStorage = $"{title}-{DateTime.Now:yyyyMMddHHmmss}{fileExtension}";

            return fileNameForStorage;
        }

        public static string FileNameFromUrl(string url)
        {
            var uri = new Uri(url);
            return Path.GetFileName(uri.AbsolutePath);
        }


        public static async System.Threading.Tasks.Task<string> GetUrlAsync(ICloudStorage cloudStorage, IFormFile file, string url)
        {
            if (file == null) return url;

            if (!string.IsNullOrEmpty(url))
            {
                var uri = new Uri(url);
                await cloudStorage.DeleteFileAsync(Path.GetFileName(uri.AbsolutePath));
            }

            var fileName = FileNameImage("store-logo", file.FileName);
            return await cloudStorage.UploadFileAsync(file, fileName);
        }
    }
}