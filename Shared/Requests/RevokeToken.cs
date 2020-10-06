using System;
using System.ComponentModel.DataAnnotations;

namespace Shared.Requests
{
    public class RevokeToken
    {
        public RevokeToken()
        {
        }

        [Required]
        public string Token { get; set; }
    }
}
