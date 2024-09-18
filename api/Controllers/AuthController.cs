using Microsoft.AspNetCore.Mvc;
using api.Interfaces;
using api.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;

namespace api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    [EnableCors("AllowAllOrigins")]
    
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpOptions("login")]
        public IActionResult PreflightRoute()
        {
            Response.Headers.Append("Access-Control-Allow-Origin", "*");
            Response.Headers.Append("Access-Control-Allow-Methods", "POST, OPTIONS");
            return NoContent();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login model)
        {
            var user = await _authRepository.Login(model.Username, model.Password);

            if (user == null)
            {
                return Unauthorized("Invalid username or password.");
            }

            return Ok(user);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            return Ok("User successfully logged out.");
        }

        [HttpGet("check")]
        public async Task<IActionResult> CheckAuth(string username)
        {
            var user = await _authRepository.CheckAuth(username);

            if (user == null)
            {
                return Unauthorized("User not authenticated.");
            }

            return Ok(new { user });
        }
    }
}