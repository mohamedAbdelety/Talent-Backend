using Microsoft.AspNetCore.Http;
using Service.IServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util.Models;

namespace Service.Services
{
    public class FileService : IFileService
    {
        #region Attribute
        #endregion
        public bool DeleteFile(IFormFile file)
        {
            string fileDirectoryPath = Path.Combine(Directory.GetCurrentDirectory(), GlobalConstants.ImgsUploadPath);
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
                int min = ((int)1e6), max = ((int)1e9);
                string RandomSlice = new Random().Next(min, max).ToString();
                string ConvertedFileName = DateTime.Now.ToString("dddd dd MMMM yyyy") + RandomSlice + file.FileName;
                string fileDirectoryPath = Path.Combine(Directory.GetCurrentDirectory(), GlobalConstants.ImgsUploadPath);
                if (!Directory.Exists(fileDirectoryPath))
                {
                    Directory.CreateDirectory(fileDirectoryPath);
                }
                string filePath = Path.Combine(fileDirectoryPath, ConvertedFileName);
                // open a file stream to save the file locally.
                using var fileStream = new FileStream(filePath, FileMode.Create);
                // copy the uplaoded file the directory.
                await file.CopyToAsync(fileStream);
                // return the file path.
                return ConvertedFileName;
            }
            catch
            {
                throw new Exception("Cannot open the uploaded file");
            }
        }
    }
}
