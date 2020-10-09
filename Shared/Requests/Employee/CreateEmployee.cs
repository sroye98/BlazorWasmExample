using System;
using System.ComponentModel.DataAnnotations;
using Shared.Requests.Common;

namespace Shared.Requests.Employee
{
    public class CreateEmployee
    {
        public CreateEmployee()
        {
        }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string EmailAddress { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required, MinLength(6)]
        public string Password { get; set; }

        [Required, MaxLength(20)]
        public string PhoneNumber { get; set; }

        public Address Address { get; set; }
    }
}
