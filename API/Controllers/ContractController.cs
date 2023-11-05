using AutoMapper;
using Domain.Entities.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.IServices;
using Service.Services;
using System;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractController : ControllerBase
    {
        private readonly IContractService _contractService;

        public ContractController(
            IContractService contractService
        )
        {
            _contractService = contractService;
        }
        [HttpPost]
        public async Task<IActionResult> AddContract(AddContractDto ContractDto)
        {
            var newContract = await _contractService.AddContract(ContractDto);
            return Ok(newContract);
        }
        [HttpGet("all")]
        public async Task<IActionResult> GetAllStars()
        {
            var starResponse = await _contractService.GetAllContracts();
            return Ok(starResponse);
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _contractService.Delete(id);
            return Ok();
        }
    }
}
