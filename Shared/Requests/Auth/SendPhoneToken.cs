using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Shared.Requests.Auth
{
    public class SendPhoneToken
    {
        public SendPhoneToken()
        {
        }

        [Required, RegularExpression(@"\[\[(TOKEN)\]\]", ErrorMessage = "Must contain [[TOKEN]] in the Phone Message")]
        public string PhoneMessage { get; set; }

        public Dictionary<string, string> PhoneMessageVariables { get; set; }

        [Required, Phone, DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
    }
}
