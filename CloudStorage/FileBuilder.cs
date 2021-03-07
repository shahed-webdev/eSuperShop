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
    }
}