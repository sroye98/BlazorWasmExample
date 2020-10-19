using System;
using System.ComponentModel.DataAnnotations;
using Shared.Requests.Common;

namespace Shared.Requests.Employee
{
    public class CreateEmployee
    {
        public CreateEmployee()
        {
            Address = new Address();
        }

        [Required, MaxLength(256)]
        public string FirstName { get; set; }

        [Required, MaxLength(256)]
        public string LastName { get; set; }

        [Required, DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [Required, MaxLength(50)]
        public string UserName { get; set; }

        [Required, MinLength(6), DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, MaxLength(20)]
        public string PhoneNumber { get; set; }

        public Address Address { get; set; }
    }
}
