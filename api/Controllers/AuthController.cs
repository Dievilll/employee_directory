using Microsoft.AspNetCore.Mvc;
using api.Interfaces;
using api.Models;
using System.Threading.Tasks;

namespace api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Register model)
        {
            if (await _authRepository.UserExists(model.Username))
            {
                return BadRequest("Username already exists.");
            }

            if (model.Password != model.ConfirmPassword)
            {
                return BadRequest("Passwords do not match.");
            }

            var user = new User { Username = model.Username, Role = "User" }; // Устанавливаем роль по умолчанию
            await _authRepository.Register(user, model.Password);

            return Ok("User successfully registered.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login model)
        {
            var user = await _authRepository.Login(model.Username, model.Password);

            if (user == null)
            {
                return Unauthorized("Invalid username or password.");
            }

            return Ok(user );
        }
    }
}