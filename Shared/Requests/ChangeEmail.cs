using System.ComponentModel.DataAnnotations;

namespace Shared.Requests
{
    public class ChangeEmail
    {
        public ChangeEmail()
        {
        }

        [Required, DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }
    }
}
