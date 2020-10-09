using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Shared.Requests.Auth
{
    public class SendEmailToken
    {
        public SendEmailToken()
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

        [Required, EmailAddress, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
