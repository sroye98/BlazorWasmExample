using System.ComponentModel.DataAnnotations;

namespace Shared.Requests.Account
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
