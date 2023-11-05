using API.Helpers;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.DTO;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Service.DTO;
using Service.IServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Util.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TalentController : ControllerBase
    {
        
        private readonly ITalentService _talentService;

        public TalentController(
            ITalentService talentService
        )
        {
            _talentService = talentService;
        }


        [HttpGet]
        public async Task<IActionResult> GetTalent([FromQuery] Guid id)
        {
            var talent = await _talentService.GetTalent(id);
            return Ok(talent);
        }

        [HttpPost]
        public async Task<IActionResult> AddTalent([FromForm] talentDto talentDto)
        {
            var newTalent = await _talentService.AddTalent(talentDto);
            return Ok(newTalent);
        }

        [HttpGet("GetAllTalents")]
        public async Task<IActionResult> GetAllTalents()
        { 
           return Ok(await _talentService.GetAllTalents());
        }

        [HttpGet("GetApprovedTalents")]
        public async Task<IActionResult> GetApprovedTalents([FromQuery] TalentFilterDTO talentfilter, [FromQuery] PaginationFilter pagination)
        {
            var talentResponse = await _talentService.GetTalents(talentfilter, pagination);
            return Ok(talentResponse);
        }

        [HttpPatch("ReverseApproveStatus/{id}")]
        public async Task<IActionResult> AcReverseApproveStatuscept(Guid id)
        {
            var talent = await _talentService.ReverseApproveStatus(id);
            return Ok(talent);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _talentService.Delete(id);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAll()
        {
            await _talentService.DeleteAll();
            return Ok();
        }


    }
}
