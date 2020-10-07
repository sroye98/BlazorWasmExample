using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLogic.Models;

namespace BusinessLogic.Interfaces
{
    public interface IAuthService
    {
        Task ChangeEmailAsync(string email);

        Task ChangeUserNameAsync(string userName);

        Task ChangePasswordAsync(
            string oldPassword,
            string newPassword);

        Task ConfirmEmailAsync(
            string token,
            string email);

        Task ConfirmPhoneAsync(
            string token,
            string phone);

        Task ForgotPasswordAsync(
            string urlTemplate,
            string fromEmail,
            string email,
            string emailSubject,
            Dictionary<string, string> emailSubjectVariables,
            string emailMessage,
            Dictionary<string, string> emailMessageVariables);

        Task<LoginResponse> Login2FAAsync(
            string token,
            string identifier,
            string ipAddress);

        Task<LoginResponse> LoginAsync(
            string identifier,
            string password,
            string ipAddress);

        Task LogoutAsync();

        Task RegisterAsync(
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
            bool sendPhoneConfirmation = true);

        Task<TokenResponse> RefreshToken(
            string refreshToken,
            string ipAddress);

        Task ResetPasswordAsync(
            string identifier,
            string newPassword,
            string token);

        Task RevokeTokenAsync(
            string refreshToken,
            string ipAddress);

        Task Send2FATokenAsync(
            string phoneMessage,
            Dictionary<string, string> phoneMessageVariables,
            string identifier);

        Task SendEmailTokenAsync(
            string urlTemplate,
            string fromEmail,
            string emailSubject,
            Dictionary<string, string> emailSubjectVariables,
            string emailMessage,
            Dictionary<string, string> emailMessageVariables,
            string email);

        Task SendPhoneTokenAsync(
            string phoneMessage,
            Dictionary<string, string> phoneMessageVariables,
            string phone);
    }
}
