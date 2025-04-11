using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Common.Services.Attachments
{
    public class AttachmentService : IAttachmentService
    {
        private readonly List<string> AllowedExtensions = new List<string>() { ".jpg", ".jpeg", ".png" };
        private const int MaxSize = 2_097_152;

        public string UploadImage(IFormFile File, string FolderName)
        {
            var fileExtension = Path.GetExtension(File.FileName);

            if (!AllowedExtensions.Contains(fileExtension))
                throw new Exception("Invalid File Extension");

            if (File.Length > MaxSize)
                throw new Exception("Invalid Size. Over Our Range!!");

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", FolderName);

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var filename = $"{Guid.NewGuid()}_{File.FileName}";

            var filePath = Path.Combine(folderPath, filename);
            //upload my file to server
            using var fs = new FileStream(filePath, FileMode.Create);
            File.CopyTo(fs);

            return filename;
        }

        public bool DeleteImage(string FilePath)
        {
            if(File.Exists(FilePath))
            {
                File.Delete(FilePath);
                return true;
            }
            return false;
        }
    }
}
