using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodosBackend.CommunicationModes;
using TodosBackend.Models;
using TodosBackend.Services.Abstractions;

namespace TodosBackend.Controllers
{
    [ApiController]
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

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDto request)
        {
            if (await _userService.FindByUserNameAsync(request.UserName) != null)
                return BadRequest();

            var user = new User();
            user.UserName = request.UserName;
            user.PasswordHash = request.Password;

            await _userService.CreateUserAsync(user);

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserDto request)
        {
            Console.WriteLine(request.UserName + "  " + request.Password);
            var response = await _authService.CreateAccessTokenAsync(request.UserName, request.Password);
            if (response.Success == false)
            {
                return BadRequest(response.Message);
            }

            return Ok(response.Token);
        }
    }
}
