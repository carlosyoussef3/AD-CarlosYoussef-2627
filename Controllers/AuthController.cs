using Microsoft.AspNetCore.Mvc;
using TimesheetApi.Dtos;
using TimesheetApi.Services;

namespace TimesheetApi.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var success = await _authService.RegisterAsync(dto);

            if (!success)
            {
                return Problem(
                    title: "Deze gebruikersnaam bestaat al",
                    statusCode: StatusCodes.Status409Conflict);
            }

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var token = await _authService.LoginAsync(dto);
            if (token is null)
            {
                return Unauthorized();
            }

            return Ok(token);
        }
    }
}
