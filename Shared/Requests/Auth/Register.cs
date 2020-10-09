using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Shared.Requests.Auth
{
    public class Register
    {
        public Register()
        {
        }

        [Required, RegularExpression(@"\[\[(TOKEN)\]\]", ErrorMessage = "Must contain [[TOKEN]] in the Url")]
        public string EmailUrlTemplate { get; set; }

        [Required, EmailAddress, DataType(DataType.EmailAddress)]
        public string FromEmail { get; set; }

        [Required]
        public string EmailSubject { get; set; }

        public Dictionary<string, string> EmailSubjectVariables { get; set; }

        [Required, RegularExpression(@"\[\[(CALLBACKURL)\]\]", ErrorMessage = "Must contain [[CALLBACKURL]] in the Email Message")]
        public string EmailMessage { get; set; }

        public Dictionary<string, string> EmailMessageVariables { get; set; }

        [Required, RegularExpression(@"\[\[(TOKEN)\]\]", ErrorMessage = "Must contain [[TOKEN]] in the Phone Message")]
        public string PhoneMessage { get; set; }

        public Dictionary<string, string> PhoneMessageVariables { get; set; }

        [Required, EmailAddress, DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required, Phone, DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        public string Role { get; set; }

        [Required]
        public bool SendEmailConfirmation { get; set; } = true;

        [Required]
        public bool SendPhoneConfirmation { get; set; } = true;
    }
}
