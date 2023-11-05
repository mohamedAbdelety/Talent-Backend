using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Service.IServices
{
    public interface IFileService
    {
        Task<string> GetUploadedFilePathAsync(IFormFile file);
        bool DeleteFile(IFormFile file);
    }
}
