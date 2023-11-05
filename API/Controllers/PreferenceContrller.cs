using AutoMapper;
using Domain.Entities.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.DTO;
using Service.IServices;
using System.Threading.Tasks;
using System;
using Util.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PreferenceController : ControllerBase
    {
        private readonly IPreferenceService _PreferenceService;

        public PreferenceController(
            IPreferenceService preferenceService
        )
        {
            _PreferenceService = preferenceService;
        }


        [HttpGet]
        public async Task<IActionResult> GetPreferences()
        {
            return Ok(await _PreferenceService.GetPreferences());
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetPreferencesList()
        {
            return Ok(await _PreferenceService.GetPreferencesList());
        }

        [HttpPost]
        public async Task<IActionResult> AddPreference([FromForm] PreferenceReq obj)
        {
            return Ok(await _PreferenceService.HirePerson(obj.personId));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _PreferenceService.Delete(id);
            return Ok();
        }
        [HttpDelete("all")]
        public async Task<IActionResult> DeleteAll()
        {
            await _PreferenceService.DeleteAll();
            return Ok();
        }
    }
}
