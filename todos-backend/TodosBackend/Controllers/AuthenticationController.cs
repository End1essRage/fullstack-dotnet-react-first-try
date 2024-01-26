using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodosBackend.CommunicationModes;
using TodosBackend.Models;
using TodosBackend.Services.Abstractions;

namespace TodosBackend.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("auth")]
    public class AuthenticationController : ControllerBase
    {
        private IUserService _userService;
        private IAuthenticationService _authService;
        public AuthenticationController(IAuthenticationService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<string>> Register(RegisterDto request)
        {
            if (await _userService.FindByUserNameAsync(request.UserName) != null)
                return BadRequest();

            var user = new User();
            user.Email = request.Email;
            user.UserName = request.UserName;
            user.PasswordHash = request.Password;

            await _userService.CreateUserAsync(user);

            var response = await _authService.CreateAccessTokenAsync(request.UserName, request.Password);
            SetRefreshToken(response.RefreshToken);

            return Ok(response.AccessToken);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginDto request)
        {
            var response = await _authService.CreateAccessTokenAsync(request.UserName, request.Password);

            if (response.Success == false)
            {
                return Unauthorized(response.Message);
            }

            SetRefreshToken(response.RefreshToken);

            return Ok(response.AccessToken);
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<string>> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(refreshToken))
                return Unauthorized("No token");

            var response = await _authService.CreateAccessTokenAsync(refreshToken);

            if(response.Success == false)
            {
                return Unauthorized(response.Message);
            }

            SetRefreshToken(response.RefreshToken);

            return Ok(response.AccessToken);
        }

        private void SetRefreshToken(RefreshToken refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = refreshToken.Expires
            };
            Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);
        }
    }
}
