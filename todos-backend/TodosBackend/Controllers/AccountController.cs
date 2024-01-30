using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodosBackend.Web.Services;
using TodosBackend.Web.Services.Abstractions;

namespace TodosBackend.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private IUserService _userService;
        private ICodeConfirmationService _confirmationService;
        public AccountController(IUserService userService, ICodeConfirmationService confirmationService) 
        {
            _userService = userService;
            _confirmationService = confirmationService;
        }

        [HttpPost("confirmation/request")]
        public async Task<ActionResult> CreateConfirmation()
        {
            int userId = _userService.GetCurrentUserId();
            if (userId < 0)
                return BadRequest();

            await _confirmationService.CreateAccountConfirmation(userId);

            return Ok();
        }

        [HttpPost("confirmation")]
        public async Task<ActionResult> ConfirmUser([FromQuery] string code)
        {
            int userId = _userService.GetCurrentUserId();
            if(userId < 0)
                return BadRequest();

            var result = await _confirmationService.TryConfirmAccount(userId, code);

            if(!result)
                return BadRequest();

            await _userService.ConfirmUser(userId);
            return Ok();
        }
    }
}
