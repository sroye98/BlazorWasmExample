using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Shared.Requests
{
    public class Send2FAToken
    {
        public Send2FAToken()
        {
        }

        [Required, RegularExpression(@"\[\[(TOKEN)\]\]", ErrorMessage = "Must contain [[TOKEN]] in the Phone Message")]
        public string PhoneMessage { get; set; }

        public Dictionary<string, string> PhoneMessageVariables { get; set; }

        [Required]
        public string Identifier { get; set; }
    }
}
