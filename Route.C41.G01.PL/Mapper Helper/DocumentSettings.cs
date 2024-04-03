using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis;
using System;
using System.IO;
//using static System.Net.WebRequestMethods;

namespace Route.C41.G01.PL.Mapper_Helper
{
    public static class DocumentSettings
    {
        public static string UploadFiles(IFormFile file,string folderName )
        {
            //1.Get Located Folder Path
            string FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", folderName);

            //2. Get File Name and Make it UNIQUE
            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

            //3. Get File Path
            string filePath = Path.Combine(FolderPath, fileName);

            //4. Save File as Streams : [Data Per Time]
            using var fileStream = new FileStream(filePath , FileMode.Create);

            file.CopyTo(fileStream);

            return fileName;
        }
         
        public static void DeleteFile(string fileName , string folderName ) 
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", folderName, fileName);
            if (File.Exists(filePath) )
            {
                File.Delete(filePath);
            }

        }
    }
}
