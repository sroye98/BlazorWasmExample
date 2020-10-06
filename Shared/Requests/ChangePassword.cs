using System;
using System.ComponentModel.DataAnnotations;

namespace Shared.Requests
{
    public class ChangePassword
    {
        public ChangePassword()
        {
        }

        [Required, DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required, DataType(DataType.Password)]
        public string NewPassword { get; set; }
    }
}
