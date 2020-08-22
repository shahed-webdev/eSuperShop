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
    }
}