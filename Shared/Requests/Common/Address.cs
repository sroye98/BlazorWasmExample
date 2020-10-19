using System;
using System.ComponentModel.DataAnnotations;

namespace Shared.Requests.Common
{
    public class Address
    {
        public Address()
        {
        }

        [Required, MaxLength(128)]
        public string Line1 { get; set; }

        [MaxLength(128)]
        public string Line2 { get; set; }

        [Required, MaxLength(50)]
        public string City { get; set; }

        [Required, MaxLength(2)]
        public string State { get; set; }

        [Required, MaxLength(10)]
        public string ZipCode { get; set; }
    }
}
