using InstallmentManager.Application.Requests.User;
using InstallmentManager.Application.Responses.User;
using InstallmentManager.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InstallmentManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserRequest request)
        {
            try
            {
                await _userService.Create(request);
                UserLoginResponse response = await _userService.Login(request.Username, request.Password);

                return Ok(response);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            try
            {
                UserLoginResponse response = await _userService.Login(request.Username, request.Password);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
