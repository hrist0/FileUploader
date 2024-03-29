using FileUploader.Services.Services;
using FileUploader.Web.ErorrMessages;
using FileUploader.Web.JwtHelper;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace FileUploader.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly IJwtTokenService _jwtTokenService;

        public LoginController(ILoginService loginService, IJwtTokenService jwtTokenService)
        {
            _loginService = loginService;
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            // Validate incoming request
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Check if user already exists
            var user = await _loginService.GetUser(loginRequest.Email);
            if (user != null)
                return Ok(_jwtTokenService.GenerateToken(user.Id));

            // Attempt to login
            user = await _loginService.Login(loginRequest.Email, loginRequest.Password);
            if (user == null)
                return BadRequest(ErrorMessages.InvalidCredentials);

            // Generate JWT token
            var token = _jwtTokenService.GenerateToken(user.Id);

            // Return token
            return Ok(token);
        }
    }
}
