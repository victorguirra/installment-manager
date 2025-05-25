using InstallmentManager.Application.Requests.Contract;
using InstallmentManager.Application.Services.Interfaces;
using InstallmentManager.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InstallmentManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContractController : ControllerBase
    {
        private readonly IUserContextService _userContextService;
        private readonly IContractService _contractService;

        public ContractController(IContractService contractService, IUserContextService userContextService)
        {
            _userContextService = userContextService;
            _contractService = contractService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            try
            {
                int userId = _userContextService.GetUserId();
                List<Contract> contracts = await _contractService.Get(userId);

                return Ok(contracts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateContractRequest request)
        {
            try
            {
                int userId = _userContextService.GetUserId();
                Contract contract = await _contractService.Create(userId, request);

                return Ok(contract);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                int userId = _userContextService.GetUserId();
                await _contractService.Delete(userId, id);

                return Ok("Successfully Delete Contract");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
