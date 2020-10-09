using System;
using System.Threading.Tasks;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shared.Requests.Account;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private IAuthService _authService;

        public AccountController(
            ILogger<AccountController> logger,
            IAuthService authService)
        {
            _logger = logger;
            _authService = authService;
        }

        [Authorize]
        [HttpPut("ChangeEmail")]
        public async Task<IActionResult> ChangeEmailAsync([FromBody] ChangeEmail payload)
        {
            try
            {
                await _authService.ChangeEmailAsync(payload.EmailAddress);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("ChangePassword")]
        public async Task<IActionResult> ChangePasswordAsync([FromBody] ChangePassword payload)
        {
            try
            {
                await _authService.ChangePasswordAsync(
                    payload.OldPassword,
                    payload.NewPassword);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("ChangeUsername")]
        public async Task<IActionResult> ChangeUsernameAsync([FromBody] ChangeUserName payload)
        {
            try
            {
                await _authService.ChangeUserNameAsync(payload.Username);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
