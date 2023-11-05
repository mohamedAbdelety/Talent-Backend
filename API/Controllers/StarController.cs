using AutoMapper;
using Domain.Entities.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.DTO;
using Service.IServices;
using System.Threading.Tasks;
using System;
using Util.Models;
using Service.Services;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StarController : ControllerBase
    {

        private readonly IStarService _starService;

        public StarController(
            IStarService starService
        )
        {
            _starService = starService;
        }        

        [HttpGet]
        public async Task<IActionResult> GetApprovedStars([FromQuery] StarFilterDto starFilter, [FromQuery] PaginationFilter pagination)
        {
            var starResponse = await _starService.Get(starFilter, pagination);
            return Ok(starResponse);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllStars()
        {
            var starResponse = await _starService.GetAllStars();
            return Ok(starResponse);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _starService.Delete(id);
            return Ok();
        }

        [HttpPatch("ReverseApproveStatus/{id}")]
        public async Task<IActionResult> AcReverseApproveStatuscept(Guid id)
        {
            var star = await _starService.ReverseApproveStatus(id);
            return Ok(star);
        }

        [HttpPost("import")]
        public async Task<IActionResult> Import([FromForm] StarImportDTO dto)
        {
            await _starService.ImportStarFile(dto);
            return Ok();
        }

    }
}
