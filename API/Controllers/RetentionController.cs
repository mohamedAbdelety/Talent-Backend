using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.IServices;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RetentionController : ControllerBase
    {
        private readonly IRetentionService _retentionService;

        public RetentionController(
            IRetentionService retentionService
        )
        {
            _retentionService = retentionService;
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAll()
        {
            await _retentionService.DeleteAll();
            return Ok();
        }
    }
}
