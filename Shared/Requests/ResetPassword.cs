﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Shared.Requests
{
    public class ResetPassword
    {
        public ResetPassword()
        {
        }

        [Required]
        public string Identifier { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Token { get; set; }
    }
}
