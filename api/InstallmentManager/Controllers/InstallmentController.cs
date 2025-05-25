using InstallmentManager.Application.Requests.Installment;
using InstallmentManager.Application.Services.Interfaces;
using InstallmentManager.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InstallmentManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InstallmentController : ControllerBase
    {
        private readonly IUserContextService _userContextService;
        private readonly IInstallmentService _installmentService;
        private readonly IInstallmentAnticipationService _installmentAnticipationService;

        public InstallmentController(
            IUserContextService userContextService,
            IInstallmentService installmentService,
            IInstallmentAnticipationService installmentAnticipationService
        )
        {
            _userContextService = userContextService;
            _installmentService = installmentService;
            _installmentAnticipationService = installmentAnticipationService;
        }

        [HttpGet("{contractId}")]
        [Authorize]
        public async Task<IActionResult> Get(int contractId)
        {
            try
            {
                int userId = _userContextService.GetUserId();
                List<Installment> installments = await _installmentService.Get(userId, contractId);
                
                return Ok(installments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("advance-request")]
        [Authorize]
        public async Task<IActionResult> GetAdvanceRequest()
        {
            try
            {
                int userId = _userContextService.GetUserId();
                List<InstallmentAnticipation> installmentsAnticipation = await _installmentAnticipationService.Get(userId);

                return Ok(installmentsAnticipation);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("advance-request/{id}")]
        [Authorize]
        public async Task<IActionResult> GetAdvanceRequestById(int id)
        {
            try
            {
                int userId = _userContextService.GetUserId();
                InstallmentAnticipation? installmentAnticipation = await _installmentAnticipationService.Get(userId, id);

                return Ok(installmentAnticipation);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("advance-request")]
        [Authorize]
        public async Task<IActionResult> Anticipate([FromBody] InstallmentAnticipationRequest request)
        {
            try
            {
                int userId = _userContextService.GetUserId();
                
                List<InstallmentAnticipation>? installmentAnticipations = await _installmentAnticipationService
                    .Anticipate(userId, request.InstallmentIds);

                return Ok(installmentAnticipations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("advance-request/{id}/approve")]
        [Authorize]
        public async Task<IActionResult> Approve(int id)
        {
            try
            {
                int userId = _userContextService.GetUserId();
                await _installmentAnticipationService.Approve(userId, id);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("advance-request/{id}/reject")]
        [Authorize]
        public async Task<IActionResult> Reject(int id)
        {
            try
            {
                int userId = _userContextService.GetUserId();
                await _installmentAnticipationService.Reject(userId, id);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
