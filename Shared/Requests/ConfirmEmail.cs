using System;
using System.ComponentModel.DataAnnotations;

namespace Shared.Requests
{
    public class ConfirmEmail
    {
        public ConfirmEmail()
        {
        }

        [Required]
        public string Token { get; set; }

        [Required, EmailAddress, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
