using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Shared.Requests.Auth
{
    public class ForgotPassword
    {
        public ForgotPassword()
        {
        }

        [Required, EmailAddress, DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [Required, EmailAddress, DataType(DataType.EmailAddress)]
        public string FromEmail { get; set; }

        [Required, RegularExpression(@"\[\[(TOKEN)\]\]", ErrorMessage = "Must contain [[TOKEN]] in the Url")]
        public string UrlTemplate { get; set; }

        public string EmailSubject { get; set; }

        public Dictionary<string, string> EmailSubjectVariables { get; set; }

        [Required, RegularExpression(@"\[\[(CALLBACKURL)\]\]", ErrorMessage = "Must contain [[CALLBACKURL]] in the Email Message")]
        public string EmailMessage { get; set; }

        public Dictionary<string, string> EmailMessageVariables { get; set; }
    }
}
