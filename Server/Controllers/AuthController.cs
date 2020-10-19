using System;
using System.Threading.Tasks;
using BusinessLogic.Interfaces;
using BusinessLogic.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shared.Requests.Auth;
using Shared.Responses.Auth;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private IAuthService _authService;

        public AuthController(
            ILogger<AuthController> logger,
            IAuthService authService)
        {
            _logger = logger;
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPut("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmailAsync([FromBody] ConfirmEmail payload)
        {
            try
            {
                await _authService.ConfirmEmailAsync(
                    payload.Token,
                    payload.Email);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPut("ConfirmPhone")]
        public async Task<IActionResult> ConfirmPhoneAsync([FromBody] ConfirmPhone payload)
        {
            try
            {
                await _authService.ConfirmPhoneAsync(
                    payload.Token,
                    payload.Phone);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPasswordAsync([FromBody] ForgotPassword payload)
        {
            try
            {
                await _authService.ForgotPasswordAsync(
                    payload.UrlTemplate,
                    payload.FromEmail,
                    payload.EmailAddress,
                    payload.EmailSubject,
                    payload.EmailSubjectVariables,
                    payload.EmailMessage,
                    payload.EmailMessageVariables);

                return NoContent();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] Login payload)
        {
            try
            {
                LoginResponse loginResponse = await _authService.LoginAsync(
                    payload.Identifier,
                    payload.Password,
                    ipAddress());

                AuthenticationResponse authResponse = new AuthenticationResponse
                {
                    EmailAddress = loginResponse.EmailAddress,
                    UserName = loginResponse.UserName,
                    Token = loginResponse.Tokens.JwtToken,
                    RefreshToken = loginResponse.Tokens.RefreshToken
                };

                return Ok(authResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("Login2FA")]
        public async Task<IActionResult> Login2FAAsync([FromBody] Login2FA payload)
        {
            try
            {
                LoginResponse loginResponse = await _authService.Login2FAAsync(
                    payload.Token,
                    payload.Identifier,
                    ipAddress());

                AuthenticationResponse authResponse = new AuthenticationResponse
                {
                    EmailAddress = loginResponse.EmailAddress,
                    UserName = loginResponse.UserName,
                    Token = loginResponse.Tokens.JwtToken,
                    RefreshToken = loginResponse.Tokens.RefreshToken
                };

                return Ok(authResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpDelete("Logout")]
        public async Task<IActionResult> LogoutAsync()
        {
            try
            {
                await _authService.LogoutAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] Register payload)
        {
            try
            {
                await _authService.RegisterAsync(
                    payload.EmailUrlTemplate,
                    payload.FromEmail,
                    payload.EmailSubject,
                    payload.EmailSubjectVariables,
                    payload.EmailMessage,
                    payload.EmailMessageVariables,
                    payload.PhoneMessage,
                    payload.PhoneMessageVariables,
                    payload.Email,
                    payload.UserName,
                    payload.Password,
                    payload.Phone,
                    payload.Role,
                    payload.SendEmailConfirmation,
                    payload.SendPhoneConfirmation);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPut("RefreshToken")]
        public async Task<IActionResult> RefreshTokensAsync([FromBody] RefreshToken payload)
        {
            try
            {
                TokenResponse tokenResponse = await _authService.RefreshToken(
                    payload.Token,
                    ipAddress());

                return Ok(tokenResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPut("ResetPassword")]
        public async Task<IActionResult> ResetPasswordAsync([FromBody] ResetPassword payload)
        {
            try
            {
                await _authService.ResetPasswordAsync(
                    payload.Identifier,
                    payload.Password,
                    payload.Token);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPut("RevokeToken")]
        public async Task<IActionResult> RevokeTokenAsync([FromBody] RevokeToken payload)
        {
            try
            {
                await _authService.RevokeTokenAsync(
                    payload.Token,
                    ipAddress());

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPut("Send2FAToken")]
        public async Task<IActionResult> Send2FATokenAsync([FromBody] Send2FAToken payload)
        {
            try
            {
                await _authService.Send2FATokenAsync(
                    payload.PhoneMessage,
                    payload.PhoneMessageVariables,
                    payload.Identifier);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPut("SendEmailToken")]
        public async Task<IActionResult> SendEmailTokenAsync([FromBody] SendEmailToken payload)
        {
            try
            {
                await _authService.SendEmailTokenAsync(
                    payload.EmailUrlTemplate,
                    payload.FromEmail,
                    payload.EmailSubject,
                    payload.EmailSubjectVariables,
                    payload.EmailMessage,
                    payload.EmailMessageVariables,
                    payload.Email);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPut("SendPhoneToken")]
        public async Task<IActionResult> SendPhoneTokenAsync([FromBody] SendPhoneToken payload)
        {
            try
            {
                await _authService.SendPhoneTokenAsync(
                    payload.PhoneMessage,
                    payload.PhoneMessageVariables,
                    payload.Phone);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private void setTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }

        private string ipAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }
}
