using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using Service.DTO;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Service.IServices
{
    public interface IExcelServiceUtil
    {
        Task<string> GetUploadedFilePathAsync(IFormFile file);
        bool DeleteExcelFile(IFormFile file);
    }
}
