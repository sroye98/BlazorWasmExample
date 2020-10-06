using System;
using System.ComponentModel.DataAnnotations;

namespace Shared.Requests
{
    public class Login2FA
    {
        public Login2FA()
        {
        }

        [Required]
        public string Token { get; set; }

        [Required]
        public string Identifier { get; set; }
    }
}
