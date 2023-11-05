using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using Service.IServices;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ExcelServiceUtil : IExcelServiceUtil
    {
        #region Attribute
        private const string FileUploadPath = "wwwroot\\UploadedFiles";
        #endregion
        public bool DeleteExcelFile(IFormFile file)
        {
            string fileDirectoryPath = Path.Combine(Directory.GetCurrentDirectory(), FileUploadPath);
            // get the file path.
            string filePath = Path.Combine(fileDirectoryPath, file.FileName);
            // check if file is exist 
            if (File.Exists(filePath))
            {
                // delete the file 
                File.Delete(filePath);
                return true;
            }
            else
                return false;
        }
        public async Task<string> GetUploadedFilePathAsync(IFormFile file)
        {
            try
            {
                string fileDirectoryPath = Path.Combine(Directory.GetCurrentDirectory(), FileUploadPath);
                if (!Directory.Exists(fileDirectoryPath))
                {
                    Directory.CreateDirectory(fileDirectoryPath);
                }
                string filePath = Path.Combine(fileDirectoryPath, file.FileName);
                // open a file stream to save the file locally.
                using var fileStream = new FileStream(filePath, FileMode.Create);
                // copy the uplaoded file the directory.
                await file.CopyToAsync(fileStream);
                // return the file path.
                return filePath;
            }
            catch
            {
                throw new Exception("Cannot open the uploaded file");
            }
        }
    }
}
