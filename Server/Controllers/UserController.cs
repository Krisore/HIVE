using HIVE.Server.Authentication;
using HIVE.Server.Services.Interface;
using HIVE.Shared.Model;
using HIVE.Shared.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace HIVE.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<UserSession>> Login(LoginRequest request)
        {
            var jwtAuthenticationManager = new JwtAuthenticationManager(_userService);
            if (request.Email == null) return NotFound();
            if (request.Password == null) return NotFound();
            try
            {
                var session = await jwtAuthenticationManager.GenerateJwtToken(request.Email, request.Password);
                if (session == null)
                {
                    return Unauthorized();
                }
                return Ok(session);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return NotFound();
        }
        [HttpPut]
        [Route("update/{id:int}")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<UserSession>> UpdateAccountAsync(int id, UserRegisterRequest request)
        {
            var jwtAuthenticationManager = new JwtAuthenticationManager(_userService);
            var result = await _userService.UpdateUser(id, request);
            var session = await jwtAuthenticationManager.GenerateJwtTokenByEmail(result);
            return Ok(session);
        }

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserRegisterRequest request)
        {
            var result = await _userService.RegisterUser(request);
            return Ok(result);
        }
        [HttpPost]
        [Route("verify/{token}")]
        public async Task<IActionResult> Verify(string token)
        {
            var verification = await _userService.Verify(token);
            return Ok(verification);

        }
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            var result = await _userService.ResetPassword(request);
            if (string.IsNullOrWhiteSpace(result))
            {
                return BadRequest("Invalid Token");
            }
            return Ok("PasswordSuccessfully reset.");
        }
        [HttpPost("forgot-password/{email}")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var result = await _userService.ForgotPasswordAsync(email);
            if (string.IsNullOrWhiteSpace(result))
            {
                return BadRequest($" Error! No Email found named {email}");
            }
            return Ok(result);
        }
        [HttpGet]
        [Route("Account/{email}")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<User>> MyAccount(string email)
        {
            var result = await _userService.GetUserAccountByEmail(email);
            if (result is null)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
        [HttpGet]
        [Route("emails")]
        public async Task<ActionResult<List<string>>> GetEmails()
        {
            var emails = await _userService.UsersEmail();
            return Ok(emails);
        }
    }
}
