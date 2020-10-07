using System.ComponentModel.DataAnnotations;

namespace Shared.Requests
{
    public class Login
    {
        public Login()
        {
        }

        [Required, DataType(DataType.Text)]
        public string Identifier { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
