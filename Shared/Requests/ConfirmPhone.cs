﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Shared.Requests
{
    public class ConfirmPhone
    {
        public ConfirmPhone()
        {
        }

        [Required]
        public string Token { get; set; }

        [Required, Phone, DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
    }
}
