using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BusinessLogic.Helpers;
using BusinessLogic.Interfaces;
using BusinessLogic.Models;
using DataLogic.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Services
{
    public class AuthService : IAuthService
    {
        private readonly string _twoFactorTokenProvider = "TwoFactorProvider";
        private readonly string _loginProvider = "LoginProvider";
        private readonly string _tokenName = "AuthToken";

        private readonly IHttpContextAccessor _httpContext;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ICommunicationService _communicationService;
        private readonly ITokenService _tokenService;

        public AuthService(
            IHttpContextAccessor httpContext,
            SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager,
            ICommunicationService communicationService,
            ITokenService tokenService)
        {
            _httpContext = httpContext;
            _signInManager = signInManager;
            _userManager = userManager;
            _communicationService = communicationService;
            _tokenService = tokenService;
        }

        /// <summary>
        /// Change Email
        /// </summary>
        /// <param name="email">New email</param>
        /// <returns></returns>
        public async Task ChangeEmailAsync(string email)
        {
            try
            {
                if (!_httpContext.HttpContext.User.Identity.IsAuthenticated)
                {
                    throw new Exception("User not logged in");
                }

                var userIdentifier = _httpContext.HttpContext.User.Identity.Name
                    ?? _httpContext.HttpContext.User.Claims.FirstOrDefault(m => m.Type == ClaimTypes.NameIdentifier).Value;

                AppUser currentUser = await _userManager.FindByNameAsync(userIdentifier)
                    ?? await _userManager.FindByEmailAsync(userIdentifier)
                    ?? await _userManager.FindByIdAsync(userIdentifier);

                if (currentUser == null)
                {
                    throw new Exception("User not registered");
                }

                string changeEmailToken = await _userManager.GenerateChangeEmailTokenAsync(
                    currentUser,
                    email);

                IdentityResult identityResult = await _userManager.ChangeEmailAsync(
                    currentUser,
                    email,
                    changeEmailToken);

                if (!identityResult.Succeeded)
                {
                    string message = string.Join(
                        "; ",
                        identityResult.Errors.Select(m => m.Description).ToArray());

                    throw new Exception(message);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Change Password
        /// </summary>
        /// <param name="oldPassword">Old password</param>
        /// <param name="newPassword">New password</param>
        /// <returns></returns>
        public async Task ChangePasswordAsync(
            string oldPassword,
            string newPassword)
        {
            try
            {
                if (!_httpContext.HttpContext.User.Identity.IsAuthenticated)
                {
                    throw new Exception("User not logged in");
                }

                var userIdentifier = _httpContext.HttpContext.User.Identity.Name
                    ?? _httpContext.HttpContext.User.Claims.FirstOrDefault(m => m.Type == ClaimTypes.NameIdentifier).Value;

                AppUser currentUser = await _userManager.FindByNameAsync(userIdentifier)
                    ?? await _userManager.FindByEmailAsync(userIdentifier)
                    ?? await _userManager.FindByIdAsync(userIdentifier);

                if (currentUser == null)
                {
                    throw new Exception("User not registered");
                }

                IdentityResult identityResult = await _userManager.ChangePasswordAsync(
                    currentUser,
                    oldPassword,
                    newPassword);

                if (!identityResult.Succeeded)
                {
                    string message = string.Join(
                        "; ",
                        identityResult.Errors.Select(m => m.Description).ToArray());

                    throw new Exception(message);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Change User Name
        /// </summary>
        /// <param name="userName">User Name</param>
        /// <returns></returns>
        public async Task ChangeUserNameAsync(string userName)
        {
            try
            {
                if (!_httpContext.HttpContext.User.Identity.IsAuthenticated)
                {
                    throw new Exception("User not logged in");
                }

                var userIdentifier = _httpContext.HttpContext.User.Identity.Name
                    ?? _httpContext.HttpContext.User.Claims.FirstOrDefault(m => m.Type == ClaimTypes.NameIdentifier).Value;

                AppUser currentUser = await _userManager.FindByNameAsync(userIdentifier)
                    ?? await _userManager.FindByEmailAsync(userIdentifier)
                    ?? await _userManager.FindByIdAsync(userIdentifier);

                if (currentUser == null)
                {
                    throw new Exception("User not registered");
                }

                currentUser.UserName = userName;

                IdentityResult identityResult = await _userManager.UpdateAsync(currentUser);

                if (!identityResult.Succeeded)
                {
                    string message = string.Join(
                        "; ",
                        identityResult.Errors.Select(m => m.Description).ToArray());

                    throw new Exception(message);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Confirm Email
        /// </summary>
        /// <param name="token">Generated token</param>
        /// <param name="email">Email</param>
        /// <returns></returns>
        public async Task ConfirmEmailAsync(
            string token,
            string email)
        {
            try
            {
                AppUser currentUser = await _userManager.FindByEmailAsync(email);

                if (currentUser == null)
                {
                    throw new Exception("User not registered");
                }

                IdentityResult identityResult = await _userManager.ConfirmEmailAsync(
                    currentUser,
                    token);

                if (!identityResult.Succeeded)
                {
                    string message = string.Join(
                        "; ",
                        identityResult.Errors.Select(m => m.Description).ToArray());

                    throw new Exception(message);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Confirm Phone
        /// </summary>
        /// <param name="token">Generated token</param>
        /// <param name="phone">Phone</param>
        /// <returns></returns>
        public async Task ConfirmPhoneAsync(
            string token,
            string phone)
        {
            try
            {
                AppUser currentUser = await _userManager.Users.FirstOrDefaultAsync(m => m.PhoneNumber == phone);

                if (currentUser == null)
                {
                    throw new Exception("User not registered");
                }

                currentUser.PhoneNumberConfirmed = await _userManager.VerifyChangePhoneNumberTokenAsync(
                    currentUser,
                    token,
                    phone);

                IdentityResult identityResult = await _userManager.UpdateAsync(currentUser);

                if (!identityResult.Succeeded)
                {
                    string message = string.Join(
                        "; ",
                        identityResult.Errors.Select(m => m.Description).ToArray());

                    throw new Exception(message);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Forgot Password
        /// </summary>
        /// <param name="urlTemplate"></param>
        /// <param name="fromEmail"></param>
        /// <param name="email"></param>
        /// <param name="emailSubject"></param>
        /// <param name="emailSubjectVariables"></param>
        /// <param name="emailMessage"></param>
        /// <param name="emailMessageVariables"></param>
        /// <returns></returns>
        public async Task ForgotPasswordAsync(
            string urlTemplate,
            string fromEmail,
            string email,
            string emailSubject,
            Dictionary<string, string> emailSubjectVariables,
            string emailMessage,
            Dictionary<string, string> emailMessageVariables)
        {
            try
            {
                AppUser currentUser = await _userManager.FindByEmailAsync(email);

                if (currentUser == null)
                {
                    throw new Exception("User not registered");
                }

                string token = await _userManager.GeneratePasswordResetTokenAsync(currentUser);

                string callbackUrl = Utilities.ParseTemplate(
                    urlTemplate,
                    new Dictionary<string, string>
                    {
                        { "Token", token }
                    });

                emailSubject = Utilities.ParseTemplate(
                    emailSubject,
                    emailSubjectVariables);

                emailMessage = Utilities.ParseTemplate(
                    emailMessage,
                    Utilities.AddParseVariables(
                        emailMessageVariables,
                        new KeyValuePair<string, string>(
                            "CallbackUrl",
                            callbackUrl)));

                await _communicationService.SendEmail(
                    email,
                    fromEmail,
                    emailSubject,
                    emailMessage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Login 2 Form Authentication
        /// </summary>
        /// <param name="token">Generated token</param>
        /// <param name="identifier">User identifier</param>
        /// <returns></returns>
        public async Task<LoginResponse> Login2FAAsync(
            string token,
            string identifier,
            string ipAddress)
        {
            try
            {
                AppUser currentUser = await _userManager.FindByNameAsync(identifier)
                    ?? await _userManager.FindByEmailAsync(identifier);

                if (currentUser == null)
                {
                    throw new Exception("User not registered");
                }

                bool verifyResult = await _userManager.VerifyTwoFactorTokenAsync(
                    currentUser,
                    _twoFactorTokenProvider,
                    token);

                if (!verifyResult)
                {
                    throw new Exception("Invalid token");
                }

                TokenResponse jwtToken = await _tokenService.GenerateTokens(
                    currentUser,
                    ipAddress);

                await _userManager.SetAuthenticationTokenAsync(
                    currentUser,
                    _loginProvider,
                    _tokenName,
                    jwtToken.JwtToken);

                return new LoginResponse
                {
                    EmailAddress = currentUser.Email,
                    UserName = currentUser.UserName,
                    Tokens = jwtToken
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="identifier">User identifier</param>
        /// <param name="password">Password</param>
        /// <param name="ipAddress">IP address</param>
        /// <returns></returns>
        public async Task<LoginResponse> LoginAsync(
            string identifier,
            string password,
            string ipAddress)
        {
            try
            {
                AppUser currentUser = await _userManager.FindByNameAsync(identifier)
                    ?? await _userManager.FindByEmailAsync(identifier);

                if (currentUser == null)
                {
                    throw new Exception("User not registered");
                }

                SignInResult signInResult = await _signInManager.CheckPasswordSignInAsync(
                    currentUser,
                    password,
                    true);

                if (!signInResult.Succeeded)
                {
                    throw new Exception("Invalid username/email and/or password");
                }

                if (signInResult.IsLockedOut)
                {
                    throw new Exception("Account is locked out");
                }

                if (signInResult.IsNotAllowed)
                {
                    throw new Exception("Account is not allowed");
                }

                if (signInResult.RequiresTwoFactor)
                {
                    throw new Exception("Required 2 Form Authentication");
                }

                TokenResponse jwtToken = await _tokenService.GenerateTokens(
                    currentUser,
                    ipAddress);

                await _userManager.SetAuthenticationTokenAsync(
                    currentUser,
                    _loginProvider,
                    _tokenName,
                    jwtToken.JwtToken);

                return new LoginResponse
                {
                    EmailAddress = currentUser.Email,
                    UserName = currentUser.UserName,
                    Tokens = jwtToken
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Logout
        /// </summary>
        /// <returns></returns>
        public async Task LogoutAsync()
        {
            try
            {
                if (!_httpContext.HttpContext.User.Identity.IsAuthenticated)
                {
                    throw new Exception("User not logged in");
                }

                var userIdentifier = _httpContext.HttpContext.User.Identity.Name
                    ?? _httpContext.HttpContext.User.Claims.FirstOrDefault(m => m.Type == ClaimTypes.NameIdentifier).Value;

                AppUser currentUser = await _userManager.FindByNameAsync(userIdentifier)
                    ?? await _userManager.FindByEmailAsync(userIdentifier)
                    ?? await _userManager.FindByIdAsync(userIdentifier);

                if (currentUser == null)
                {
                    throw new Exception("User not registered");
                }

                IdentityResult identityResult = await _userManager.UpdateSecurityStampAsync(currentUser);

                if (!identityResult.Succeeded)
                {
                    throw new Exception("User seucirty stamp failed to update");
                }

                identityResult = await _userManager.RemoveAuthenticationTokenAsync(
                    currentUser,
                    _loginProvider,
                    _tokenName);

                await _signInManager.SignOutAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Register
        /// </summary>
        /// <param name="emailUrlTemplate"></param>
        /// <param name="fromEmail"></param>
        /// <param name="emailSubject"></param>
        /// <param name="emailSubjectVariables"></param>
        /// <param name="emailMessage"></param>
        /// <param name="emailMessageVariables"></param>
        /// <param name="phoneUrlTemplate"></param>
        /// <param name="phoneMessage"></param>
        /// <param name="phoneMessageVariables"></param>
        /// <param name="email"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="phone"></param>
        /// <param name="role"></param>
        /// <param name="sendEmailConfirmation"></param>
        /// <param name="sendPhoneConfirmation"></param>
        /// <returns></returns>
        public async Task RegisterAsync(
            string emailUrlTemplate,
            string fromEmail,
            string emailSubject,
            Dictionary<string, string> emailSubjectVariables,
            string emailMessage,
            Dictionary<string, string> emailMessageVariables,
            string phoneMessage,
            Dictionary<string, string> phoneMessageVariables,
            string email,
            string userName,
            string password,
            string phone,
            string role,
            bool sendEmailConfirmation = true,
            bool sendPhoneConfirmation = true)
        {
            try
            {
                AppUser currentUser = await _userManager.FindByNameAsync(userName)
                    ?? await _userManager.FindByEmailAsync(email);

                if (currentUser != null)
                {
                    throw new Exception("User already registered");
                }

                AppUser newUser = new AppUser
                {
                    Email = email,
                    UserName = userName,
                    PhoneNumber = phone
                };

                IdentityResult identityResult = await _userManager.CreateAsync(
                    newUser,
                    password);

                if (!identityResult.Succeeded)
                {
                    throw new Exception("User was not registered");
                }

                if (!string.IsNullOrEmpty(role))
                {
                    identityResult = await _userManager.AddToRoleAsync(
                        newUser,
                        role);
                }

                if (sendEmailConfirmation)
                {
                    string emailToken = await _userManager.GenerateEmailConfirmationTokenAsync(currentUser);

                    string emailCallbackUrl = Utilities.ParseTemplate(
                        emailUrlTemplate,
                        new Dictionary<string, string>
                        {
                            { "Token", emailToken }
                        });

                    emailSubject = Utilities.ParseTemplate(
                        emailSubject,
                        emailSubjectVariables);

                    emailMessage = Utilities.ParseTemplate(
                        emailMessage,
                        Utilities.AddParseVariables(
                            emailMessageVariables,
                            new KeyValuePair<string, string>(
                                "CallbackUrl",
                                emailCallbackUrl)));

                    await _communicationService.SendEmail(
                        newUser.Email,
                        fromEmail,
                        emailSubject,
                        emailMessage);
                }

                if (sendPhoneConfirmation)
                {
                    string phoneToken = await _userManager.GenerateChangePhoneNumberTokenAsync(
                        currentUser,
                        phone);

                    phoneMessage = Utilities.ParseTemplate(
                        phoneMessage,
                        Utilities.AddParseVariables(
                            phoneMessageVariables,
                            new KeyValuePair<string, string>(
                                "Token",
                                phoneToken)));

                    await _communicationService.SendSMS(
                        phone,
                        phoneMessage);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Refresh Token
        /// </summary>
        /// <param name="refreshToken">Refresh token</param>
        /// <param name="ipAddress">IP address</param>
        /// <returns></returns>
        public async Task<TokenResponse> RefreshToken(
            string refreshToken,
            string ipAddress)
        {
            try
            {
                AppUser user = await _userManager.Users
                    .Include(m => m.RefreshTokens)
                    .FirstOrDefaultAsync(m => m.RefreshTokens.Any(m => m.Token == refreshToken));

                if (user == null)
                {
                    throw new Exception("User not registered");
                }

                var refreshTokenObj = user.RefreshTokens.Single(x => x.Token == refreshToken);

                if (!refreshTokenObj.IsActive)
                {
                    throw new Exception("Refresh token is not active");
                }    

                refreshTokenObj.Revoked = DateTime.UtcNow;
                refreshTokenObj.RevokedByIp = ipAddress;
                refreshTokenObj.ReplacedByToken = refreshToken;
                await _userManager.UpdateAsync(user);

                TokenResponse tokenResponse = await _tokenService.GenerateTokens(
                    user,
                    ipAddress);

                return tokenResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            

            throw new NotImplementedException();
        }

        /// <summary>
        /// Reset Password
        /// </summary>
        /// <param name="identifier">User identifier</param>
        /// <param name="newPassword">New password</param>
        /// <param name="token">Token</param>
        /// <returns></returns>
        public async Task ResetPasswordAsync(
            string identifier,
            string newPassword,
            string token)
        {
            try
            {
                AppUser currentUser = await _userManager.FindByNameAsync(identifier)
                    ?? await _userManager.FindByEmailAsync(identifier);

                if (currentUser == null)
                {
                    throw new Exception("User not registered");
                }

                IdentityResult identityResult = await _userManager.ResetPasswordAsync(currentUser, token, newPassword);

                if (!identityResult.Succeeded)
                {
                    string message = string.Join(
                        "; ",
                        identityResult.Errors.Select(m => m.Description).ToArray());

                    throw new Exception(message);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Revoke Token
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public async Task RevokeTokenAsync(
            string refreshToken,
            string ipAddress)
        {
            try
            {
                AppUser user = await _userManager.Users
                    .Include(m => m.RefreshTokens)
                    .FirstOrDefaultAsync(m => m.RefreshTokens.Any(m => m.Token == refreshToken));

                if (user == null)
                {
                    throw new Exception("User not registered");
                }

                var refreshTokenObj = user.RefreshTokens.Single(x => x.Token == refreshToken);

                if (!refreshTokenObj.IsActive)
                {
                    throw new Exception("Refresh token not active");
                }

                refreshTokenObj.Revoked = DateTime.UtcNow;
                refreshTokenObj.RevokedByIp = ipAddress;

                await _userManager.UpdateAsync(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Send 2 Form Authentication
        /// </summary>
        /// <param name="urlTemplate"></param>
        /// <param name="phoneMessage"></param>
        /// <param name="phoneMessageVariables"></param>
        /// <param name="identifier"></param>
        /// <returns></returns>
        public async Task Send2FATokenAsync(
            string phoneMessage,
            Dictionary<string, string> phoneMessageVariables,
            string identifier)
        {
            try
            {
                AppUser currentUser = await _userManager.FindByNameAsync(identifier)
                    ?? await _userManager.FindByEmailAsync(identifier);

                if (currentUser == null)
                {
                    throw new Exception("User not registered");
                }

                string token = await _userManager.GenerateTwoFactorTokenAsync(
                    currentUser,
                    _twoFactorTokenProvider);

                phoneMessage = Utilities.ParseTemplate(
                    phoneMessage,
                    Utilities.AddParseVariables(
                        phoneMessageVariables,
                        new KeyValuePair<string, string>(
                            "Token",
                            token)));

                await _communicationService.SendSMS(
                    currentUser.PhoneNumber,
                    phoneMessage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Send Email Token
        /// </summary>
        /// <param name="urlTemplate"></param>
        /// <param name="fromEmail"></param>
        /// <param name="emailSubject"></param>
        /// <param name="emailSubjectVariables"></param>
        /// <param name="emailMessage"></param>
        /// <param name="emailMessageVariables"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task SendEmailTokenAsync(
            string urlTemplate,
            string fromEmail,
            string emailSubject,
            Dictionary<string, string> emailSubjectVariables,
            string emailMessage,
            Dictionary<string, string> emailMessageVariables,
            string email)
        {
            try
            {
                AppUser currentUser = await _userManager.FindByEmailAsync(email);

                if (currentUser == null)
                {
                    throw new Exception("User not registered");
                }

                string token = await _userManager.GenerateEmailConfirmationTokenAsync(currentUser);

                string emailCallbackUrl = Utilities.ParseTemplate(
                    urlTemplate,
                    new Dictionary<string, string>
                    {
                            { "Token", token }
                    });

                emailSubject = Utilities.ParseTemplate(
                    emailSubject,
                    emailSubjectVariables);

                emailMessage = Utilities.ParseTemplate(
                    emailMessage,
                    Utilities.AddParseVariables(
                        emailMessageVariables,
                        new KeyValuePair<string, string>(
                            "CallbackUrl",
                            emailCallbackUrl)));

                await _communicationService.SendEmail(
                    currentUser.Email,
                    fromEmail,
                    emailSubject,
                    emailMessage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Send Phone Token
        /// </summary>
        /// <param name="urlTemplate"></param>
        /// <param name="phoneMessage"></param>
        /// <param name="phoneMessageVariables"></param>
        /// <param name="phone"></param>
        /// <returns></returns>
        public async Task SendPhoneTokenAsync(
            string phoneMessage,
            Dictionary<string, string> phoneMessageVariables,
            string phone)
        {
            try
            {
                AppUser currentUser = await _userManager.Users.FirstOrDefaultAsync(m => m.PhoneNumber == phone);

                if (currentUser == null)
                {
                    throw new Exception("User not registered");
                }

                string token = await _userManager.GenerateChangePhoneNumberTokenAsync(
                    currentUser,
                    _twoFactorTokenProvider);

                phoneMessage = Utilities.ParseTemplate(
                    phoneMessage,
                    Utilities.AddParseVariables(
                        phoneMessageVariables,
                        new KeyValuePair<string, string>(
                            "Token",
                            token)));

                await _communicationService.SendSMS(
                    phone,
                    phoneMessage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
